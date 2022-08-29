using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniProjetBibliotheque.Data;
using MiniProjetBibliotheque.Models;

namespace MiniProjetBibliotheque.Controllers
{
    public class EmpruntsController : Controller
    {
        private readonly DbContextBibliotheque _context;

        public EmpruntsController(DbContextBibliotheque context)
        {
            _context = context;
        }

        // GET: Emprunts
        //public async Task<IActionResult> Index()
        //{
        //    var dbContextBibliotheque = _context.Emprunts.Include(e => e.Lecteur).Include(e => e.Livre);
        //    return View(await dbContextBibliotheque.ToListAsync());
        //}
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var emprunts = from a in _context.Emprunts.Include(e => e.Lecteur).Include(e => e.Livre)
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                emprunts = emprunts.Where(s => s.Livre.Titre.Contains(searchString));
            }

            return View(await emprunts.ToListAsync());
        }

        // GET: Emprunts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emprunts == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts
                .Include(e => e.Lecteur)
                .Include(e => e.Livre)
                .FirstOrDefaultAsync(m => m.EmpruntId == id);
            if (emprunt == null)
            {
                return NotFound();
            }

            return View(emprunt);
        }

        // GET: Emprunts/Create
        public IActionResult Create()
        {
            ViewData["LecteurId"] = new SelectList(_context.Lecteurs, "LecteurId", "Mdp_Lecteur");
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "Titre");
            return View();
        }

        // POST: Emprunts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpruntId,LecteurId,LivreId,DateEmprunt,DateRetour")] Emprunt emprunt)
        {
            if (!ModelState.IsValid)
            { 
                if(emprunt.DateRetour > emprunt.DateEmprunt)
                {
                    _context.Add(emprunt);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Emprunt ajoutée avec succès";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["AlertErreur"] = "La date de retour doit etre supérieur de la date d'emprunt ";


                }
                
            }
            ViewData["LecteurId"] = new SelectList(_context.Lecteurs, "LecteurId", "Mdp_Lecteur", emprunt.LecteurId);
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "Titre", emprunt.LivreId);
            return View(emprunt);
        }

        // GET: Emprunts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emprunts == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt == null)
            {
                return NotFound();
            }
            ViewData["LecteurId"] = new SelectList(_context.Lecteurs, "LecteurId", "Mdp_Lecteur", emprunt.LecteurId);
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "Titre", emprunt.LivreId);
            return View(emprunt);
        }

        // POST: Emprunts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpruntId,LecteurId,LivreId,DateEmprunt,DateRetour")] Emprunt emprunt)
        {
            if (id != emprunt.EmpruntId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprunt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpruntExists(emprunt.EmpruntId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["AlertMessage"] = "Emprunt modifiée avec succès";
                return RedirectToAction(nameof(Index));
            }
            ViewData["LecteurId"] = new SelectList(_context.Lecteurs, "LecteurId", "Mdp_Lecteur", emprunt.LecteurId);
            ViewData["LivreId"] = new SelectList(_context.Livres, "LivreId", "Titre", emprunt.LivreId);
            return View(emprunt);
        }

        // GET: Emprunts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emprunts == null)
            {
                return NotFound();
            }

            var emprunt = await _context.Emprunts
                .Include(e => e.Lecteur)
                .Include(e => e.Livre)
                .FirstOrDefaultAsync(m => m.EmpruntId == id);
            if (emprunt == null)
            {
                return NotFound();
            }

            return View(emprunt);
        }

        // POST: Emprunts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emprunts == null)
            {
                return Problem("Entity set 'DbContextBibliotheque.Emprunts'  is null.");
            }
            var emprunt = await _context.Emprunts.FindAsync(id);
            if (emprunt != null)
            {
                _context.Emprunts.Remove(emprunt);
            }
            
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Emprunt supprimée avec succès";
            return RedirectToAction(nameof(Index));
        }

        private bool EmpruntExists(int id)
        {
          return (_context.Emprunts?.Any(e => e.EmpruntId == id)).GetValueOrDefault();
        }
    }
}
