using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Controllers
{
    public class AirportController(IAirportRepository airportRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await airportRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await airportRepository.GetByIdAsync(id.Value);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Airport airport)
        {
            if (ModelState.IsValid)
            {
                await airportRepository.CreateAsync(airport);
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await airportRepository.GetByIdAsync(id.Value);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Airport airport)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await airportRepository.UpdateAsync(airport);

                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await airportRepository.GetByIdAsync(id.Value);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await airportRepository.GetByIdAsync(id);
            if (airport == null)
            {
                return NotFound();
            }

            try
            {
                await airportRepository.DeleteAsync(airport);
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Não foi possível excluir, pois existem dados relacionados.";
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
