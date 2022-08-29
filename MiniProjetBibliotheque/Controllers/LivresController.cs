﻿using System;
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
    public class LivresController : Controller
    {
        private readonly DbContextBibliotheque _context;

        public LivresController(DbContextBibliotheque context)
        {
            _context = context;
        }

        // GET: Livres
        //public async Task<IActionResult> Index()
        //{
        //    var dbContextBibliotheque = _context.Livres.Include(l => l.Auteur).Include(l => l.Domaine);
        //    return View(await dbContextBibliotheque.ToListAsync());
        //}
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var livres = from a in _context.Livres.Include(l => l.Auteur).Include(l => l.Domaine)
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                livres = livres.Where(s => s.Titre.Contains(searchString));
            }

            return View(await livres.ToListAsync());
        }

        // GET: Livres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livres == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Domaine)
                .Include(l => l.EmpruntsLivre)
                .FirstOrDefaultAsync(m => m.LivreId == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // GET: Livres/Create
        public IActionResult Create()
        {
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "NomAuteur");
            ViewData["DomaineId"] = new SelectList(_context.Domaines, "DomaineId", "NomDomaine");
            return View();
        }

        // POST: Livres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivreId,Titre,NombrePages,AuteurId,DomaineId")] Livre livre)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(livre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "NomAuteur", livre.AuteurId);
            ViewData["DomaineId"] = new SelectList(_context.Domaines, "DomaineId", "NomDomaine", livre.DomaineId);

            return View(livre);
        }

        // GET: Livres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livres == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres.FindAsync(id);
            if (livre == null)
            {
                return NotFound();
            }
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "NomAuteur", livre.AuteurId);
            ViewData["DomaineId"] = new SelectList(_context.Domaines, "DomaineId", "NomDomaine", livre.DomaineId);
            return View(livre);
        }

        // POST: Livres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivreId,Titre,NombrePages,AuteurId,DomaineId")] Livre livre)
        {
            if (id != livre.LivreId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(livre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivreExists(livre.LivreId))
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
            ViewData["AuteurId"] = new SelectList(_context.Auteurs, "AuteurId", "NomAuteur", livre.AuteurId);
            ViewData["DomaineId"] = new SelectList(_context.Domaines, "DomaineId", "NomDomaine", livre.DomaineId);
            return View(livre);
        }

        // GET: Livres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livres == null)
            {
                return NotFound();
            }

            var livre = await _context.Livres
                .Include(l => l.Auteur)
                .Include(l => l.Domaine)
                .FirstOrDefaultAsync(m => m.LivreId == id);
            if (livre == null)
            {
                return NotFound();
            }

            return View(livre);
        }

        // POST: Livres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Livres == null)
            {
                return Problem("Entity set 'DbContextBibliotheque.Livres'  is null.");
            }
            var livre = await _context.Livres.FindAsync(id);
            if (livre != null)
            {
                _context.Livres.Remove(livre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivreExists(int id)
        {
          return (_context.Livres?.Any(e => e.LivreId == id)).GetValueOrDefault();
        }
    }
}
