using AtlasAir.Data;
using AtlasAir.Interfaces;
using AtlasAir.Models;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Repositories
{
    public class ReservationRepository(AtlasAirDbContext context) : IReservationRepository
    {
        public async Task CreateAsync(Reservation reservation)
        {
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            context.Reservations.Remove(reservation);
            await context.SaveChangesAsync();
        }

        public async Task<List<Reservation>?> GetAllAsync()
        {
            return await context.Reservations.ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await context.Reservations.FindAsync(id);
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            context.Reservations.Update(reservation);
            await context.SaveChangesAsync();
        }
    }
}
