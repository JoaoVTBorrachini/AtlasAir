using AtlasAir.Enums;
using AtlasAir.Models;
using Microsoft.EntityFrameworkCore;

namespace AtlasAir.Data
{
    public class AtlasAirDbContext(DbContextOptions<AtlasAirDbContext> options) : DbContext(options)
    {
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightSegment> FlightSegments { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aircraft>().ToTable("Aircraft");

            modelBuilder.Entity<Airport>().ToTable("Airport");

            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Flight>().ToTable("Flight");
            modelBuilder.Entity<Flight>()
                .Property(f => f.Status)
                .HasConversion<string>()
                .HasDefaultValue(FlightStatus.Scheduled);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.OriginAirport)
                .WithMany()
                .HasForeignKey(f => f.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DestinationAirport)
                .WithMany()
                .HasForeignKey(f => f.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightSegment>().ToTable("FlightSegment");
            modelBuilder.Entity<FlightSegment>()
                .HasOne(fs => fs.Flight)
                .WithMany(f => f.FlightSegments)
                .HasForeignKey(fs => fs.FlightId);
            modelBuilder.Entity<FlightSegment>()
                .HasOne(fs => fs.Aircraft)
                .WithMany(a => a.FlightSegments)
                .HasForeignKey(fs => fs.AircraftId);
            modelBuilder.Entity<FlightSegment>()
                .HasOne(fs => fs.OriginAirport)
                .WithMany()
                .HasForeignKey(fs => fs.OriginAirportId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FlightSegment>()
                .HasOne(fs => fs.DestinationAirport)
                .WithMany()
                .HasForeignKey(fs => fs.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Status)
                .HasConversion<string>()
                .HasDefaultValue(ReservationStatus.Pending);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Seat)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.SeatId);
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Flight)
                .WithMany(f => f.Reservations) 
                .HasForeignKey(r => r.FlightId);

            modelBuilder.Entity<Seat>().ToTable("Seat");
            modelBuilder.Entity<Seat>()
                .Property(s => s.Class)
                .HasConversion<string>();
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Aircraft)
                .WithMany(a => a.Seats)
                .HasForeignKey(s => s.AircraftId);
        }
    }
}