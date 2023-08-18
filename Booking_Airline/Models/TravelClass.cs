using System.Runtime.CompilerServices;

namespace Booking_Airline.Models
{
    public class TravelClass
    {
        public int Id { get; set; }
        public int TravelClassID { get; set; }
        public string TravelClassName { get; set; }
        public ICollection<ServiceForClass> ServiceForClasses { get; set; }
    }
}
