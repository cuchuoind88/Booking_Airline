namespace Booking_Airline.Models
{
    public class ServiceForClass
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ServcieDescription { get; set; }
        public ICollection<TravelClass> TravelClasses { get; set; }
    }
}
