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
    public class MediaAvaliacoesController : Controller
    {
        private readonly AppDbContext _context;

        public MediaAvaliacoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MediaAvaliacoes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MediaAvaliacoes.Include(m => m.Local);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MediaAvaliacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaAvaliacao = await _context.MediaAvaliacoes
                .Include(m => m.Local)
                .FirstOrDefaultAsync(m => m.idMediaAvaliacao == id);
            if (mediaAvaliacao == null)
            {
                return NotFound();
            }

            return View(mediaAvaliacao);
        }

        // GET: MediaAvaliacoes/Create
        public IActionResult Create()
        {
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco");
            return View();
        }

        // POST: MediaAvaliacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idMediaAvaliacao,idLocal,QtdAvaliacoes,VlUltimaAvaliacao,VlUltimaAvaliacaoMedia,AvaliacaoMedia")] MediaAvaliacao mediaAvaliacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaAvaliacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", mediaAvaliacao.idLocal);
            return View(mediaAvaliacao);
        }

        // GET: MediaAvaliacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaAvaliacao = await _context.MediaAvaliacoes.FindAsync(id);
            if (mediaAvaliacao == null)
            {
                return NotFound();
            }
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", mediaAvaliacao.idLocal);
            return View(mediaAvaliacao);
        }

        // POST: MediaAvaliacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idMediaAvaliacao,idLocal,QtdAvaliacoes,VlUltimaAvaliacao,VlUltimaAvaliacaoMedia,AvaliacaoMedia")] MediaAvaliacao mediaAvaliacao)
        {
            if (id != mediaAvaliacao.idMediaAvaliacao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaAvaliacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaAvaliacaoExists(mediaAvaliacao.idMediaAvaliacao))
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
            ViewData["idLocal"] = new SelectList(_context.Locais, "idLocal", "Endereco", mediaAvaliacao.idLocal);
            return View(mediaAvaliacao);
        }

        // GET: MediaAvaliacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaAvaliacao = await _context.MediaAvaliacoes
                .Include(m => m.Local)
                .FirstOrDefaultAsync(m => m.idMediaAvaliacao == id);
            if (mediaAvaliacao == null)
            {
                return NotFound();
            }

            return View(mediaAvaliacao);
        }

        // POST: MediaAvaliacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaAvaliacao = await _context.MediaAvaliacoes.FindAsync(id);
            if (mediaAvaliacao != null)
            {
                _context.MediaAvaliacoes.Remove(mediaAvaliacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaAvaliacaoExists(int id)
        {
            return _context.MediaAvaliacoes.Any(e => e.idMediaAvaliacao == id);
        }

    }

}
