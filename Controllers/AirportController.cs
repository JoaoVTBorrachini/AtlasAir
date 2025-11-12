using Microsoft.AspNetCore.Mvc;
using AtlasAir.Models;
using AtlasAir.Interfaces;

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
        public async Task<IActionResult> Create([Bind("Id,Name,Street,Neighborhood,City,State,Country,ZipCode")] Airport airport)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Street,Neighborhood,City,State,Country,ZipCode")] Airport airport)
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
            if (airport != null)
            {
                await airportRepository.DeleteAsync(airport);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
