
using Booking_Airline.Serivces.AirportServicesss;
using Booking_Airline.Serivces.AuthenticationService;
using Booking_Airline.Serivces.CoutriesService;

namespace Booking_Airline.Serivces.ServiceManager
{
    public interface IServiceManger
    {
       IAuthenService AuthenService { get; }
       ICountriesSerivce CountriesSerivce { get; }
       IAirportService AirportService { get; }

    }
}
