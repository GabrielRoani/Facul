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
            var appDbContext = _context.Avaliacoes
                .Include(a => a.Local)
                .Include(a => a.Usuario);
            return View(await appDbContext.ToListAsync());
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
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco");
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome");
            return View();
        }

        // POST: Avaliacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idAvaliacao,DescricaoAvaliacao,ValorAvaliacao,Data,idUsuario,idLocal")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dashboard", "Homepage");
            }
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", avaliacao.idLocal);
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", avaliacao.idUsuario);
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
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", avaliacao.idLocal);
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", avaliacao.idUsuario);
            return View(avaliacao);
        }

        // POST: Avaliacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idAvaliacao,DescricaoAvaliacao,ValorAvaliacao,Data,idUsuario,idLocal")] Avaliacao avaliacao)
        {
            if (id != avaliacao.idAvaliacao)
            {
                return NotFound();
            }

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
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", avaliacao.idLocal);
            ViewData["idUsuario"] = new SelectList(_context.Usuarios, "id", "Nome", avaliacao.idUsuario);
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

            return View(avaliacao);
        }

        // POST: Avaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Dashboard", "Homepage");
        }

        private bool AvaliacaoExists(int id)
        {
            return _context.Avaliacoes.Any(e => e.idAvaliacao == id);
        }
    }
}