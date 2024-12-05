using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionDesProduits.Data;
using GestionDesProduits.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GestionDesProduits.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MagasinsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MagasinsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Magasins
        public async Task<IActionResult> Index()
        {
              return View(await _context.Magasin.ToListAsync());
        }

        // GET: Magasins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Magasin == null)
            {
                return NotFound();
            }

            var magasin = await _context.Magasin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magasin == null)
            {
                return NotFound();
            }

            return View(magasin);
        }

        // GET: Magasins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Magasins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomMagasin,ville")] Magasin magasin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magasin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magasin);
        }

        // GET: Magasins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Magasin == null)
            {
                return NotFound();
            }

            var magasin = await _context.Magasin.FindAsync(id);
            if (magasin == null)
            {
                return NotFound();
            }
            return View(magasin);
        }

        // POST: Magasins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomMagasin,ville")] Magasin magasin)
        {
            if (id != magasin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magasin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagasinExists(magasin.Id))
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
            return View(magasin);
        }

        // GET: Magasins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Magasin == null)
            {
                return NotFound();
            }

            var magasin = await _context.Magasin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (magasin == null)
            {
                return NotFound();
            }

            return View(magasin);
        }

        // POST: Magasins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Magasin == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Magasin'  is null.");
            }
            var magasin = await _context.Magasin.FindAsync(id);
            if (magasin != null)
            {
                _context.Magasin.Remove(magasin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagasinExists(int id)
        {
          return _context.Magasin.Any(e => e.Id == id);
        }
    }
}
