using Microsoft.EntityFrameworkCore;

namespace Booking_Airline.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reservation>  Reservations { get; set; }
        public DbSet<SeatDetails> SeatDetails { get; set; }
        public DbSet<ServiceForClass> ServiceForClasses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TravelClass> TravelClasses { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<FlightDetail>()
                .HasOne(f => f.SourceAirPort)
                .WithMany(a => a.Flights)
                .HasForeignKey(fd => fd.SourceAirportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FlightDetail>()
                .HasOne(f => f.DestinationAirport)
                .WithMany()
                .HasForeignKey(fd => fd.DestinationAirportId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
