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
    public class LecteursController : Controller
    {
        private readonly DbContextBibliotheque _context;

        public LecteursController(DbContextBibliotheque context)
        {
            _context = context;
        }

        // GET: Lecteurs
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Lecteurs != null ? 
        //                  View(await _context.Lecteurs.ToListAsync()) :
        //                  Problem("Entity set 'DbContextBibliotheque.Lecteurs'  is null.");
        //}
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var lecteurs = from a in _context.Lecteurs
                          select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                lecteurs = lecteurs.Where(s => s.NomLecteur.Contains(searchString)
                                       || s.PrenomLecteur.Contains(searchString));
            }

            return View(await lecteurs.ToListAsync());
        }

        // GET: Lecteurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lecteurs == null)
            {
                return NotFound();
            }

            var lecteur = await _context.Lecteurs
                .Include(s => s.EmpruntsLecteur)
                .FirstOrDefaultAsync(m => m.LecteurId == id);
            if (lecteur == null)
            {
                return NotFound();
            }

            return View(lecteur);
        }

        // GET: Lecteurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lecteurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LecteurId,NomLecteur,PrenomLecteur,EmailLecteur,TelephoneLecteur,Adresse,Mdp_Lecteur")] Lecteur lecteur)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(lecteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecteur);
        }

        // GET: Lecteurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lecteurs == null)
            {
                return NotFound();
            }

            var lecteur = await _context.Lecteurs.FindAsync(id);
            if (lecteur == null)
            {
                return NotFound();
            }
            return View(lecteur);
        }

        // POST: Lecteurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LecteurId,NomLecteur,PrenomLecteur,EmailLecteur,TelephoneLecteur,Adresse,Mdp_Lecteur")] Lecteur lecteur)
        {
            if (id != lecteur.LecteurId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecteurExists(lecteur.LecteurId))
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
            return View(lecteur);
        }

        // GET: Lecteurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lecteurs == null)
            {
                return NotFound();
            }

            var lecteur = await _context.Lecteurs
                .FirstOrDefaultAsync(m => m.LecteurId == id);
            if (lecteur == null)
            {
                return NotFound();
            }

            return View(lecteur);
        }

        // POST: Lecteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lecteurs == null)
            {
                return Problem("Entity set 'DbContextBibliotheque.Lecteurs'  is null.");
            }
            var lecteur = await _context.Lecteurs.FindAsync(id);
            if (lecteur != null)
            {
                _context.Lecteurs.Remove(lecteur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }

        private bool LecteurExists(int id)
        {
          return (_context.Lecteurs?.Any(e => e.LecteurId == id)).GetValueOrDefault();
        }
    }
}
