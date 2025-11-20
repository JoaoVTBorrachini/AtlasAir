using AtlasAir.Data;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Repositories
{
    public class FlightRepository(AtlasAirDbContext context) : IFlightRepository
    {
        public async Task CreateAsync(Flight flight)
        {
            await context.Flights.AddAsync(flight);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Flight flight)
        {
            context.Flights.Remove(flight);
            await context.SaveChangesAsync();
        }

        public async Task<List<Flight>?> GetAllAsync()
        {
            return await context.Flights
                .Include(f => f.OriginAirport)
                .Include(f => f.DestinationAirport)
                .Include(f => f.FlightSegments)
                    .ThenInclude(fs => fs.OriginAirport)
                .Include(f => f.FlightSegments)
                    .ThenInclude(fs => fs.DestinationAirport)
                .ToListAsync();
        }

        public async Task<Flight?> GetByIdAsync(int id)
        {
            return await context.Flights
                .Include(f => f.OriginAirport)
                .Include(f => f.DestinationAirport)
                .Include(f => f.FlightSegments)
                    .ThenInclude(fs => fs.OriginAirport)
                .Include(f => f.FlightSegments)
                    .ThenInclude(fs => fs.DestinationAirport)
                .Include(f => f.FlightSegments)
                    .ThenInclude(fs => fs.Aircraft)
                .AsSplitQuery()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Flight>?> GetFlightsByRouteAsync(int originAirportId, int destinationAirportId)
        {
            return await context.Flights
                .Where(f => f.OriginAirportId == originAirportId && f.DestinationAirportId == destinationAirportId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            context.Flights.Update(flight);
            await context.SaveChangesAsync();
        }
    }
}
