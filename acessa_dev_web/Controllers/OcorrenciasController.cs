using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using acessa_dev_web.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization; 


namespace acessa_dev_web.Controllers
{
    [Authorize]
    public class OcorrenciasController : Controller
    {
        private readonly AppDbContext _context;

        public OcorrenciasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ocorrencias
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ocorrencias
                .Include(o => o.Local)
                .Include(o => o.Usuario);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Ocorrencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocorrencia = await _context.Ocorrencias
                .Include(o => o.Local)
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.idOcorrencia == id);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            return View(ocorrencia);
        }

        // GET: Ocorrencias/Create
        public IActionResult Create()
        {
            // Garantir que os dados estão sendo carregados
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Nome");

            var locais = _context.Locais
                .Select(l => new { l.idLocal, l.Nome, l.Endereco, l.Latitude, l.Longitude })
                .ToList();
            ViewBag.LocaisJson = System.Text.Json.JsonSerializer.Serialize(locais);

            // Obtém o id e nome do usuário autenticado
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == userId);

            ViewBag.UsuarioId = userId;
            ViewBag.UsuarioNome = usuario?.Nome ?? "Usuário";

            return View();
        }

        // POST: Ocorrencias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idOcorrencia,DescricaoOcorrencia,Categoria,Severidade,Status,Data,idUsuario,idLocal")] Ocorrencia ocorrencia, [Bind("Nome,Endereco,Latitude,Longitude")] Local local)
        {
            if (ModelState.IsValid)
            {

                // Verifica se já existe um local com o mesmo Nome e Endereco
                var localExistente = await _context.Locais
                        .FirstOrDefaultAsync(l => l.Nome == local.Nome && l.Endereco == local.Endereco);

                if (localExistente == null)
                {
                    // Se não existe, adiciona o novo local
                    _context.Locais.Add(local);
                    await _context.SaveChangesAsync();
                    ocorrencia.idLocal = local.idLocal; // associa o novo local à ocorrência
                }
                else
                {
                    // Se já existe, associa o local existente à ocorrência
                    ocorrencia.idLocal = localExistente.idLocal;
                }
                _context.Add(ocorrencia);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Homepage");
            }

            // Recarregar os dados se houver erro
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Nome", ocorrencia.idLocal);
            return View(ocorrencia);
        }

        // GET: Ocorrencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (ocorrencia == null)
            {
                return NotFound();
            }
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Nome", ocorrencia.idLocal);
            //ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", ocorrencia.idUsuario);
            return View(ocorrencia);
        }

        // POST: Ocorrencias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idOcorrencia,DescricaoOcorrencia,Categoria,Severidade,Status,Data,idUsuario,idLocal")] Ocorrencia ocorrencia)
        {
            if (id != ocorrencia.idOcorrencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ocorrencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OcorrenciaExists(ocorrencia.idOcorrencia))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Dashboard", "Homepage");
            }
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", ocorrencia.idLocal);
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", ocorrencia.idUsuario);
            return View(ocorrencia);
        }

        // GET: Ocorrencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocorrencia = await _context.Ocorrencias
                .Include(o => o.Local)
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.idOcorrencia == id);
            if (ocorrencia == null)
            {
                return NotFound();
            }

            return View(ocorrencia);
        }

        // POST: Ocorrencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ocorrencia = await _context.Ocorrencias.FindAsync(id);
            if (ocorrencia != null)
            {
                _context.Ocorrencias.Remove(ocorrencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Homepage");
        }

        private bool OcorrenciaExists(int id)
        {
            return _context.Ocorrencias.Any(e => e.idOcorrencia == id);
        }
    }
}