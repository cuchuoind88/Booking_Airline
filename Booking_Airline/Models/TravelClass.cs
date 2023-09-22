using System.Runtime.CompilerServices;

namespace Booking_Airline.Models
{
    public class TravelClass
    {
        public Guid Id { get; set; }
        public string TravelClassName { get; set; }
        public ICollection<ServiceForClass> ServiceForClasses { get; set; }
        public ICollection<TicketPrice> TicketPrices { get; set; }
    }
}
