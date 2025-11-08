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
            return await context.Flights.ToListAsync();
        }

        public async Task<Flight?> GetByIdAsync(int id)
        {
            return await context.Flights.FindAsync(id);
        }

        public async Task UpdateAsync(Flight flight)
        {
            context.Flights.Update(flight);
            await context.SaveChangesAsync();
        }
    }
}
