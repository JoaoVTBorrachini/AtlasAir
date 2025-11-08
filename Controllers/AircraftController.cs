using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AtlasAir.Data;
using AtlasAir.Models;
using AtlasAir.Interfaces;

namespace AtlasAir.Controllers
{
    public class AircraftController(IAircraftRepository aircraftRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await aircraftRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await aircraftRepository.GetByIdAsync(id.Value);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Model,SeatCount")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                await aircraftRepository.CreateAsync(aircraft);
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await aircraftRepository.GetByIdAsync(id.Value);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,SeatCount")] Aircraft aircraft)
        {
            if (id != aircraft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await aircraftRepository.UpdateAsync(aircraft);
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await aircraftRepository.GetByIdAsync(id.Value);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await aircraftRepository.GetByIdAsync(id);
            if (aircraft != null)
            {
                await aircraftRepository.DeleteAsync(aircraft);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
