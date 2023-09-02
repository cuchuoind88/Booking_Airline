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
        public DbSet<Country> Countries{ get; set; }
        public DbSet<TokenRemainLogin> TokenRemainLogins { get; set; }
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
            modelBuilder.Entity<TokenRemainLogin>()
               .HasOne(t =>t.User )
               .WithMany(u=>u.tokenRemainLogins)
               .HasForeignKey(tk => tk.UserId)
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Airport>()
              .HasOne(a => a.Country)
              .WithMany(c => c.Airports)
              .HasForeignKey(ap => ap.CountryId);
            //Relation between FlightDetails and TicketPrice
            modelBuilder.Entity<TicketPrice>()
              .HasOne(a => a.FlightDetail)
              .WithMany(c => c.TickerPrices)
              .HasForeignKey(tk => tk.FlightId);
            //Relation between TicketPrice and TraveClass
            modelBuilder.Entity<TicketPrice>()
              .HasOne(a => a.TravelClass)
              .WithMany(c => c.TicketPrices)
              .HasForeignKey(tk => tk.ClassID);
            //Relation between AdditionalFoodService and FlightDetails 
               modelBuilder.Entity<FoodForFlight>().HasKey( ff => new {  ff.FlightId ,ff.FoodId });
            //Relation to management FoodService and Reservation
              modelBuilder.Entity<ReservationMapAddionalFoodService>().HasKey(rs => new { rs.ReservationId ,rs.AdditionalFoodServiceId });
        }
    }
}
