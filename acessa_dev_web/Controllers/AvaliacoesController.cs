using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using acessa_dev_web.Models;
using System.Security.Claims;

namespace acessa_dev_web.Controllers
{
    public class AvaliacoesController : Controller
    {
        private readonly AppDbContext _context;

        public AvaliacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Avaliacoes
        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _context.Avaliacoes
                .Include(a => a.Local)
                .Include(a => a.Usuario)
                .ToListAsync();

            // Cálculo da média de notas por local
            var mediasPorLocal = avaliacoes
                .GroupBy(a => a.Local.Endereco)
                .Select(g => new
                {
                    Local = g.Key,
                    Media = g.Average(a => a.ValorAvaliacao)
                })
                .ToList();

            // Envia as médias para a View
            ViewBag.MediasPorLocal = mediasPorLocal
    .Select(m => new Dictionary<string, object>
    {
        { "Local", m.Local },
        { "Media", m.Media }
    })
    .ToList();


            return View(avaliacoes);
        }


        // GET: Avaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Local)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.idAvaliacao == id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            return View(avaliacao);
        }

        // GET: Avaliacoes/Create
        public IActionResult Create()
        {
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

        // POST: Avaliacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idAvaliacao,DescricaoAvaliacao,ValorAvaliacao,Data,idUsuario,idLocal")] Avaliacao avaliacao, [Bind("Nome,Endereco,Latitude,Longitude")] Local local)
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
                    avaliacao.idLocal = local.idLocal; // associa o novo local à avaliação
                }
                else
                {
                    // Se já existe, associa o local existente à avaliação
                    avaliacao.idLocal = localExistente.idLocal;
                }

                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Homepage");
            }

            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Nome", avaliacao.idLocal);
            return View(avaliacao);
        }

        // GET: Avaliacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            // Busca o local relacionado
            var local = await _context.Locais.FindAsync(avaliacao.idLocal);
            ViewBag.Nome = local?.Nome;
            ViewBag.Endereco = local?.Endereco;
            ViewBag.Latitude = local?.Latitude;
            ViewBag.Longitude = local?.Longitude;

            // Verifica se o usuário logado é o dono da avaliação
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || avaliacao.idUsuario != int.Parse(userIdClaim.Value))
            {
                TempData["MensagemErro"] = "Você não tem permissão para editar esta avaliação.";
                return RedirectToAction("Index");
            }

            // Obtém o id e nome do usuário autenticado           
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            var usuario = _context.Usuarios.FirstOrDefault(u => u.id == userId);

            ViewBag.UsuarioId = userId;
            ViewBag.UsuarioNome = usuario?.Nome ?? "Usuário";

            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Nome", avaliacao.idLocal);
            return View(avaliacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idAvaliacao,DescricaoAvaliacao,ValorAvaliacao,Data,idLocal")] Avaliacao avaliacao)
        {
            if (id != avaliacao.idAvaliacao)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int idUsuarioLogado = int.Parse(userIdClaim.Value);

            var avaliacaoExistente = await _context.Avaliacoes.AsNoTracking().FirstOrDefaultAsync(a => a.idAvaliacao == id);
            if (avaliacaoExistente == null)
            {
                return NotFound();
            }

            // Verifica se pertence ao usuário logado
            if (avaliacaoExistente.idUsuario != idUsuarioLogado)
            {
                TempData["MensagemErro"] = "Você não tem permissão para editar esta avaliação.";
                return RedirectToAction("Index");
            }

            avaliacao.idUsuario = idUsuarioLogado;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvaliacaoExists(avaliacao.idAvaliacao))
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

            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Nome", avaliacao.idLocal);
            return View(avaliacao);
        }

        // GET: Avaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avaliacao = await _context.Avaliacoes
                .Include(a => a.Local)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.idAvaliacao == id);

            if (avaliacao == null)
            {
                return NotFound();
            }

            // Verifica se o usuário logado é o dono da avaliação
            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null || avaliacao.idUsuario != int.Parse(userIdClaim.Value))
            {
                TempData["MensagemErro"] = "Você não tem permissão para excluir esta avaliação.";
                return RedirectToAction("Index");
            }

            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst("id") ?? User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int idUsuarioLogado = int.Parse(userIdClaim.Value);

            // Garante que só o dono possa excluir
            if (avaliacao.idUsuario != idUsuarioLogado)
            {
                TempData["MensagemErro"] = "Você não tem permissão para excluir esta avaliação.";
                return RedirectToAction("Index");
            }

            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return RedirectToAction("Dashboard", "Homepage");
        }
        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.idAvaliacao == id);
        }

    }
}