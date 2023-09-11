using Booking_Airline.Models;
using Booking_Airline.Repository.RepositoryBase;

namespace Booking_Airline.Repository.AirportRepository
{
    public class AirportRepository : RepositoryBase<Airport> , IAirportRepository
    { 
        public AirportRepository(ApplicationDbContext context) : base(context) { }

    }
    
}
