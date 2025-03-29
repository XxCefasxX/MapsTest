using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapsTest.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MapsTest.Controllers
{
    public class MarcadoresController : Controller
    {
        private readonly MapsContext _context;

        public MarcadoresController(MapsContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult GetMarcadores(string colonia="", string calle1 = "", string calle2 = "")
        {
            TempData["colonia"] = colonia;
            TempData["calle1"] = calle1;
            TempData["calle2"] = calle2;
            return RedirectToAction("Index");
            //return View("Index", marcadores);
        }


        // GET: Marcadores
        public async Task<IActionResult> Index()
        {
            string colonia = TempData["colonia"]?.ToString() ?? "";
            string calle1 = TempData["calle1"]?.ToString() ?? "";
            string calle2 = TempData["calle2"]?.ToString() ?? "";


            ViewBag.colonia = colonia;
            ViewBag.calle1 = calle1;
            ViewBag.calle2 = calle2;

            var query = _context.Marcadores.AsQueryable();

            if (!string.IsNullOrEmpty(colonia))
            {
                query = query.Where(m => m.Colonia.Contains(colonia));
            }
            if (!string.IsNullOrEmpty(calle1) || !string.IsNullOrEmpty(calle2))
            {
                query = query.Where(m =>
                    (string.IsNullOrEmpty(calle1) || m.Calle1.Contains(calle1) || m.Calle2.Contains(calle1)) &&
                    (string.IsNullOrEmpty(calle2) || m.Calle1.Contains(calle2) || m.Calle2.Contains(calle2))
                );
            }

            var marcadores = query.ToListAsync();
            return View(await marcadores);
        }

        // GET: Marcadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcador = await _context.Marcadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marcador == null)
            {
                return NotFound();
            }

            return View(marcador);
        }

        // GET: Marcadores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Latitud,Longitud")] Marcador marcador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marcador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marcador);
        }

        // GET: Marcadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcador = await _context.Marcadores.FindAsync(id);
            if (marcador == null)
            {
                return NotFound();
            }
            return View(marcador);
        }

        // POST: Marcadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,Latitud,Longitud")] Marcador marcador)
        {
            if (id != marcador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marcador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcadorExists(marcador.Id))
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
            return View(marcador);
        }

        // GET: Marcadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marcador = await _context.Marcadores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marcador == null)
            {
                return NotFound();
            }

            return View(marcador);
        }

        // POST: Marcadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marcador = await _context.Marcadores.FindAsync(id);
            if (marcador != null)
            {
                _context.Marcadores.Remove(marcador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcadorExists(int id)
        {
            return _context.Marcadores.Any(e => e.Id == id);
        }
    }
}
