using AtlasAir.Data;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Repositories
{
    public class SeatRepository(AtlasAirDbContext context) : ISeatRepository
    {
        public async Task CreateAsync(Seat seat)
        {
            await context.Seats.AddAsync(seat);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seat seat)
        {
            context.Seats.Remove(seat);
            await context.SaveChangesAsync();
        }

        public async Task<List<Seat>?> GetAllAsync()
        {
            return await context.Seats.ToListAsync();
        }

        public async Task<List<Seat>?> GetAvailableSeatsByFlightIdAsync(int flightId)
        {
            return await context.Seats
                .Where(s => s.Aircraft.FlightSegments.Any(fs => fs.FlightId == flightId) &&
                            !context.Reservations.Any(r => r.FlightId == flightId && r.SeatId == s.Id))
                .ToListAsync();
        }

        public async Task<Seat?> GetByIdAsync(int id)
        {
            return await context.Seats.FindAsync(id);
        }

        public async Task UpdateAsync(Seat seat)
        {
            context.Seats.Update(seat);
            await context.SaveChangesAsync();
        }
    }
}
