using Booking_Airline.Serivces.AuthenticationService;


namespace Booking_Airline.Serivces.ServiceManager
{
    public interface IServiceManger
    {
       IAuthenService AuthenService { get; }

    }
}
