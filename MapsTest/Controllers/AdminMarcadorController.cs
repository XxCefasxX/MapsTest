using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapsTest.Models;

namespace MapsTest.Controllers
{
    public class AdminMarcadorController : Controller
    {
        private readonly MapsContext _context;

        public AdminMarcadorController(MapsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Crearmarcador([Bind("Id,Titulo,Colonia,Calle1,Calle2,Descripcion,Latitud,Longitud,Categoria,Precio")] Marcador marcador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marcador);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
            //return View("Index", marcadores);
        }



        // GET: AdminMarcador
        public async Task<IActionResult> Index()
        {

            // Opciones para el dropdown (ejemplo con categorías de marcadores)
            ViewBag.Categorias = new List<SelectListItem>
            {
                new SelectListItem { Text = "Azul", Value = "1" },
                new SelectListItem { Text = "Rojo", Value = "2" },
                new SelectListItem { Text = "Verde", Value = "3" }
            };
            ViewBag.Marcador = new Marcador();
            return View(await _context.Marcadores.ToListAsync());
        }

        // GET: AdminMarcador/Details/5
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

        // GET: AdminMarcador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminMarcador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Colonia,Calle1,Calle2,Descripcion,Latitud,Longitud,Categoria,Precio")] Marcador marcador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marcador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marcador);
        }

        // GET: AdminMarcador/Edit/5
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

        // POST: AdminMarcador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Colonia,Calle1,Calle2,Descripcion,Latitud,Longitud,Categoria,Precio")] Marcador marcador)
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

        // GET: AdminMarcador/Delete/5
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

        // POST: AdminMarcador/Delete/5
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
