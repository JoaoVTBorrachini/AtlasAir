using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AtlasAir.Models;
using AtlasAir.Interfaces;

namespace AtlasAir.Controllers
{
    public class FlightSegmentController(IFlightSegmentRepository flightSegmentRepository, IAircraftRepository aircraftRepository, IAirportRepository airportRepository, IFlightRepository flightRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await flightSegmentRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightSegment = await flightSegmentRepository.GetByIdAsync(id.Value);
            if (flightSegment == null)
            {
                return NotFound();
            }

            return View(flightSegment);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["AircraftId"] = new SelectList(await aircraftRepository.GetAllAsync(), "Id", "Id");
            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id");
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id");
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,FlightId,AircraftId,SegmentOrder,OriginAirportId,DestinationAirportId,DepartureTime,ArrivalTime")] FlightSegment flightSegment)
        {
            if (ModelState.IsValid)
            {
                await flightSegmentRepository.CreateAsync(flightSegment);
                return RedirectToAction(nameof(Index));
            }

            ViewData["AircraftId"] = new SelectList(await aircraftRepository.GetAllAsync(), "Id", "Id", flightSegment.AircraftId);
            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flightSegment.DestinationAirportId);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", flightSegment.FlightId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flightSegment.OriginAirportId);
            return View(flightSegment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightSegment = await flightSegmentRepository.GetByIdAsync(id.Value);
            if (flightSegment == null)
            {
                return NotFound();
            }

            ViewData["AircraftId"] = new SelectList(await aircraftRepository.GetAllAsync(), "Id", "Id", flightSegment.AircraftId);
            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flightSegment.DestinationAirportId);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", flightSegment.FlightId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flightSegment.OriginAirportId);
            return View(flightSegment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FlightId,AircraftId,SegmentOrder,OriginAirportId,DestinationAirportId,DepartureTime,ArrivalTime")] FlightSegment flightSegment)
        {
            if (id != flightSegment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await flightSegmentRepository.UpdateAsync(flightSegment);
                return RedirectToAction(nameof(Index));
            }

            ViewData["AircraftId"] = new SelectList(await aircraftRepository.GetAllAsync(), "Id", "Id", flightSegment.AircraftId);
            ViewData["DestinationAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flightSegment.DestinationAirportId);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", flightSegment.FlightId);
            ViewData["OriginAirportId"] = new SelectList(await airportRepository.GetAllAsync(), "Id", "Id", flightSegment.OriginAirportId);
            return View(flightSegment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightSegment = await flightSegmentRepository.GetByIdAsync(id.Value);
            if (flightSegment == null)
            {
                return NotFound();
            }

            return View(flightSegment);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightSegment = await flightSegmentRepository.GetByIdAsync(id);
            if (flightSegment != null)
            {
                await flightSegmentRepository.DeleteAsync(flightSegment);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
