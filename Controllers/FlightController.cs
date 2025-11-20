using AtlasAir.Enums;
using AtlasAir.Helpers;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Controllers
{
    public class FlightController(IFlightRepository flightRepository, IAirportRepository airportRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await flightRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var flight = await flightRepository.GetByIdAsync(id.Value);

            if (flight == null)
                return NotFound();

            return View(flight);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name");
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (ModelState.IsValid)
            {
                await flightRepository.CreateAsync(flight);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", flight.DestinationAirportId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", flight.OriginAirportId);
            ViewData["StatusOptions"] = new SelectList(Enum.GetValues(typeof(FlightStatus)));

            return View(flight);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await flightRepository.GetByIdAsync(id.Value);
            if (flight == null)
            {
                return NotFound();
            }

            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", flight.DestinationAirportId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", flight.OriginAirportId);

            var statusValues = Enum.GetValues(typeof(FlightStatus)).Cast<FlightStatus>();
            var statusOptions = statusValues.Select(s => new SelectListItem
            {
                Text = s.GetDisplayName(),
                Value = s.ToString()
            });

            ViewData["StatusOptions"] = new SelectList(statusOptions, "Value", "Text");

            return View(flight);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await flightRepository.UpdateAsync(flight);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", flight.DestinationAirportId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", flight.OriginAirportId);

            var statusValues = Enum.GetValues(typeof(FlightStatus)).Cast<FlightStatus>();
            var statusOptions = statusValues.Select(s => new SelectListItem
            {
                Text = s.GetDisplayName(),
                Value = s.ToString()
            });

            ViewData["StatusOptions"] = new SelectList(statusOptions, "Value", "Text");

            return View(flight);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await flightRepository.GetByIdAsync(id.Value);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await flightRepository.GetByIdAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            try
            {
                await flightRepository.DeleteAsync(flight);
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Não foi possível excluir, pois existem dados relacionados.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
