using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AtlasAir.Models;
using AtlasAir.Interfaces;

namespace AtlasAir.Controllers
{
    public class ReservationController(IReservationRepository reservationRepository, ICustomerRepository customerRepository, IFlightRepository flightRepository, ISeatRepository seatRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await reservationRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await reservationRepository.GetByIdAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await customerRepository.GetAllAsync(), "Id", "Id", null);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", null);
            ViewData["SeatId"] = new SelectList(await seatRepository.GetAllAsync(), "Id", "Id", null);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ReservationCode,CustomerId,SeatId,FlightId,Status,ReservationDate,CancellationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await reservationRepository.CreateAsync(reservation);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(await customerRepository.GetAllAsync(), "Id", "Id", reservation.CustomerId);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", reservation.FlightId);
            ViewData["SeatId"] = new SelectList(await seatRepository.GetAllAsync(), "Id", "Id", reservation.SeatId);
            return View(reservation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await reservationRepository.GetByIdAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(await customerRepository.GetAllAsync(), "Id", "Id", reservation.CustomerId);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", reservation.FlightId);
            ViewData["SeatId"] = new SelectList(await seatRepository.GetAllAsync(), "Id", "Id", reservation.SeatId);
            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservationCode,CustomerId,SeatId,FlightId,Status,ReservationDate,CancellationDate")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await reservationRepository.UpdateAsync(reservation);
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(await customerRepository.GetAllAsync(), "Id", "Id", reservation.CustomerId);
            ViewData["FlightId"] = new SelectList(await flightRepository.GetAllAsync(), "Id", "Id", reservation.FlightId);
            ViewData["SeatId"] = new SelectList(await seatRepository.GetAllAsync(), "Id", "Id", reservation.SeatId);
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await reservationRepository.GetByIdAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await reservationRepository.GetByIdAsync(id);
            if (reservation != null)
            {
                await reservationRepository.DeleteAsync(reservation);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
