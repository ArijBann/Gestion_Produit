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

namespace GestionDesProduits.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produits.Include(p => p.Categories).Include(p => p.Magasins);
            return View(await applicationDbContext.ToListAsync());
        }


        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var toutLesProduits = await _context.Produits.Include(m => m.Categories).Include(m => m.Magasins).ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = toutLesProduits.Where(n => n.NomProduit.ToLower().Contains(searchString.ToLower()) || n.Categories.NomCategorie.ToLower().Contains(searchString.ToLower())).ToList();
                return View("Index", filteredResult);
            }

            return View("Index", toutLesProduits);
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits
                .Include(p => p.Categories)
                .Include(p => p.Magasins)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produits == null)
            {
                return NotFound();
            }

            return View(produits);
        }
        [Authorize(Roles = "Admin")]
        //[Authorize(Roles ="SuperAdmin")]
        // GET: Produits/Create
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "NomCategorie");
            ViewData["MagasinId"] = new SelectList(_context.Magasin, "Id", "NomParVille");
            return View();
        }

        // POST: Produits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id,ImageUrl,NomProduit,MagasinId,CategorieId,DescriptionStock,DateDebutPromo,DateFinPromo,prixProduit,prixProduitEnPromo")] Produits produits)
        {
            
            if (ModelState.IsValid)
            {
                Boolean test = true;
                if(produits.prixProduitEnPromo >= produits.prixProduit)
                {
                    test = false;
                    ViewBag.InvalidPrix = "Le prix est invalide";
                }
                if (produits.DateDebutPromo >= produits.DateFinPromo)
                {
                    test = false;
                    ViewBag.InvalidDate = "La date est invalide";
                }
                if(test)
                {
                    _context.Add(produits);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "NomCategorie", produits.CategorieId);
            ViewData["MagasinId"] = new SelectList(_context.Magasin, "Id", "NomParVille", produits.MagasinId);
            return View(produits);
        }

        // GET: Produits/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits.FindAsync(id);
            if (produits == null)
            {
                return NotFound();
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "NomCategorie", produits.CategorieId);
            ViewData["MagasinId"] = new SelectList(_context.Magasin, "Id", "NomParVille", produits.MagasinId);
            return View(produits);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,NomProduit,MagasinId,CategorieId,DescriptionStock,DateDebutPromo,DateFinPromo,prixProduit,prixProduitEnPromo")] Produits produits)
        {
            if (id != produits.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                    Boolean test = true;
                    if (produits.prixProduitEnPromo >= produits.prixProduit)
                    {
                        test = false;
                        ViewBag.InvalidPrix = "Le prix est invalide";
                    }
                    if (produits.DateDebutPromo >= produits.DateFinPromo)
                    {
                        test = false;
                        ViewBag.InvalidDate = "La date est invalide";
                    }
                    if (test)
                    {
                        try
                        {
                                 _context.Update(produits);
                                 await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                                if (!ProduitsExists(produits.Id))
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
                
            }
            ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "NomCategorie", produits.CategorieId);
            ViewData["MagasinId"] = new SelectList(_context.Magasin, "Id", "NomParVille", produits.MagasinId);
            return View(produits);
        }

        // GET: Produits/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produits == null)
            {
                return NotFound();
            }

            var produits = await _context.Produits
                .Include(p => p.Categories)
                .Include(p => p.Magasins)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produits == null)
            {
                return NotFound();
            }

            return View(produits);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produits == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produits'  is null.");
            }
            var produits = await _context.Produits.FindAsync(id);
            if (produits != null)
            {
                _context.Produits.Remove(produits);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitsExists(int id)
        {
          return _context.Produits.Any(e => e.Id == id);
        }
    }
}
