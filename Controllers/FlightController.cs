using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AtlasAir.Models;
using AtlasAir.Interfaces;

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

        public async Task<IActionResult> Create()
        {
            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id");
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,FlightNumber,OriginAirportId,DestinationAirportId,ScheduledDeparture,ActualDeparture,ScheduledArrival,ActualArrival,Status")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                await flightRepository.CreateAsync(flight);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flight.DestinationAirportId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flight.OriginAirportId);
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

            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flight.DestinationAirportId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flight.OriginAirportId);
            return View(flight);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightNumber,OriginAirportId,DestinationAirportId,ScheduledDeparture,ActualDeparture,ScheduledArrival,ActualArrival,Status")] Flight flight)
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

            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flight.DestinationAirportId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flight.OriginAirportId);
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
            if (flight != null)
            {
                await flightRepository.DeleteAsync(flight);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
