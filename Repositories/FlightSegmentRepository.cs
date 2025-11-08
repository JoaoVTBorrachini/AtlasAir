using AtlasAir.Data;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Repositories
{
    public class FlightSegmentRepository(AtlasAirDbContext context) : IFlightSegmentRepository
    {
        public async Task CreateAsync(FlightSegment flightSegment)
        {
            await context.FlightSegments.AddAsync(flightSegment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FlightSegment flightSegment)
        {
            context.FlightSegments.Remove(flightSegment);
            await context.SaveChangesAsync();
        }

        public async Task<List<FlightSegment>?> GetAllAsync()
        {
            return await context.FlightSegments.ToListAsync();
        }

        public async Task<FlightSegment?> GetByIdAsync(int id)
        {
            return await context.FlightSegments.FindAsync(id);
        }

        public async Task UpdateAsync(FlightSegment flightSegment)
        {
            context.FlightSegments.Update(flightSegment);
            await context.SaveChangesAsync();
        }
    }
}
