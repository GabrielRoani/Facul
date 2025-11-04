using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using acessa_dev_web.Models;

namespace acessa_dev_web.Controllers
{
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
            var appDbContext = _context.Ocorrencias.Include(o => o.Local).Include(o => o.Usuario);
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
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco");
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome");
            return View();
        }

        // POST: Ocorrencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idOcorrencia,DescricaoOcorrencia,Categoria,Severidade,Status,Data,idUsuario,idLocal")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ocorrencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", ocorrencia.idLocal);
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", ocorrencia.idUsuario);
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
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", ocorrencia.idLocal);
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", ocorrencia.idUsuario);
            return View(ocorrencia);
        }

        // POST: Ocorrencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool OcorrenciaExists(int id)
        {
            return _context.Ocorrencias.Any(e => e.idOcorrencia == id);
        }
    }
}
