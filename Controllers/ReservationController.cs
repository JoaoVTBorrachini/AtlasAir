using AtlasAir.Enums;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using AtlasAir.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Controllers
{
    public class ReservationController(
        IReservationRepository reservationRepository,
        ICustomerRepository customerRepository,
        IFlightRepository flightRepository,
        ISeatRepository seatRepository,
        IAirportRepository airportRepository
    ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await reservationRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var reservation = await reservationRepository.GetByIdAsync(id.Value);
            if (reservation == null) return NotFound();
            return View(reservation);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ReservationViewModel
            {
                AirportList = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name"),
                CustomerList = new SelectList(await customerRepository.GetAllAsync(), "Id", "Name"),
            };

            return View(viewModel);
        }

        /// <summary>
        /// Ação chamada pelo JavaScript para buscar voos com base na origem e destino.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAvailableFlights(int originId, int destinationId)
        {
            var flights = await flightRepository.GetFlightsByRouteAsync(originId, destinationId);

            var availableFlights = flights.Select(f => new ReservationViewModel.AvailableFlight
            {
                FlightId = f.Id,
                OriginAirportName = f.OriginAirport?.Name,
                DestinationAirportName = f.DestinationAirport?.Name,
                ScheduledDeparture = f.ScheduledDeparture,
                ScheduledArrival = f.ScheduledArrival
            });

            return Json(availableFlights);
        }

        /// <summary>
        /// Ação chamada pelo JavaScript para buscar assentos de um voo específico.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAvailableSeats(int flightId)
        {
            var seats = await seatRepository.GetAvailableSeatsByFlightIdAsync(flightId);

            var seatList = seats.Select(s => new { s.Id, s.SeatNumber });

            return Json(seatList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            if (viewModel.SelectedCustomerId.HasValue &&
                viewModel.SelectedFlightId.HasValue &&
                viewModel.SelectedSeatId.HasValue &&
                !string.IsNullOrEmpty(viewModel.ReservationCode))
            {
                var reservation = new Reservation
                {
                    ReservationCode = viewModel.ReservationCode,
                    CustomerId = viewModel.SelectedCustomerId.Value,
                    SeatId = viewModel.SelectedSeatId.Value,
                    FlightId = viewModel.SelectedFlightId.Value,
                    ReservationDate = DateTime.Now,
                    Status = ReservationStatus.Confirmed
                };

                await reservationRepository.CreateAsync(reservation);
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Erro ao criar reserva. Verifique todos os campos.";
            viewModel.AirportList = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name", viewModel.SelectedOriginAirportId);
            viewModel.CustomerList = new SelectList(await customerRepository.GetAllAsync(), "Id", "Name", viewModel.SelectedCustomerId);

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var reservation = await reservationRepository.GetByIdAsync(id.Value);
            if (reservation == null) return NotFound();

            // Buscar o voo para pegar origem e destino
            var flight = await flightRepository.GetByIdAsync(reservation.FlightId);
            if (flight == null) return NotFound();

            var viewModel = new ReservationViewModel
            {
                ReservationId = reservation.Id,
                ReservationCode = reservation.ReservationCode,
                SelectedCustomerId = reservation.CustomerId,
                SelectedFlightId = reservation.FlightId,
                SelectedSeatId = reservation.SeatId,
                SelectedOriginAirportId = flight.OriginAirportId,
                SelectedDestinationAirportId = flight.DestinationAirportId,

                // Popular as listas
                CustomerList = new SelectList(await customerRepository.GetAllAsync(), "Id", "Name"),
                AirportList = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name")
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationViewModel viewModel)
        {
            if (id != viewModel.ReservationId) return NotFound();

            if (ModelState.IsValid)
            {
                var reservation = await reservationRepository.GetByIdAsync(id);
                if (reservation == null) return NotFound();

                // Atualizar apenas os campos editáveis
                reservation.CustomerId = viewModel.SelectedCustomerId.Value;
                reservation.FlightId = viewModel.SelectedFlightId.Value;
                reservation.SeatId = viewModel.SelectedSeatId.Value;
                // ReservationCode geralmente não deve ser alterado, mas se quiser permitir:
                // reservation.ReservationCode = viewModel.ReservationCode;

                await reservationRepository.UpdateAsync(reservation);
                return RedirectToAction(nameof(Index));
            }

            // Recarregar as listas em caso de erro
            viewModel.CustomerList = new SelectList(await customerRepository.GetAllAsync(), "Id", "Name");
            viewModel.AirportList = new SelectList(await airportRepository.GetAllAsync(), "Id", "Name");

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var reservation = await reservationRepository.GetByIdAsync(id.Value);
            if (reservation == null) return NotFound();
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await reservationRepository.GetByIdAsync(id);
            if (reservation == null) return NotFound();

            try
            {
                await reservationRepository.DeleteAsync(reservation);
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "Não foi possível excluir, pois existem dados relacionados.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}