using AtlasAir.Enums;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AtlasAir.Controllers
{
    public class AircraftController(IAircraftRepository aircraftRepository, ISeatRepository seatRepository) : Controller
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
        public async Task<IActionResult> Create(Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                await aircraftRepository.CreateAsync(aircraft);

                for (int i = 0; i < aircraft.SeatCount; i++)
                {
                    var seat = new Seat
                    {
                        AircraftId = aircraft.Id,
                        SeatNumber = GenerateSeatNumber(i),
                        Class = DetermineSeatClass(i, aircraft.SeatCount)
                    };

                    await seatRepository.CreateAsync(seat);
                }

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
        public async Task<IActionResult> Edit(int id, Aircraft aircraft)
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

            if (aircraft == null)
            {
                return NotFound();
            }
            
            try
            {
                await aircraftRepository.DeleteAsync(aircraft);
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Não foi possível excluir, pois existem dados relacionados.";
            }

            return RedirectToAction(nameof(Index));
        }


        private static string GenerateSeatNumber(int index)
        {
            int row = (index / 6) + 1;
            char letter = (char)('A' + (index % 6));
            return $"{row}{letter}";
        }

        private static SeatClass DetermineSeatClass(int index, int totalSeats)
        {
            double percentage = (double)index / totalSeats;

            if (percentage < 0.2)
                return SeatClass.FirstClass;
            else if (percentage < 0.5)
                return SeatClass.Business;
            else
                return SeatClass.Economy;
        }
    }
}
