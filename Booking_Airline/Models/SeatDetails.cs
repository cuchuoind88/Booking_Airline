namespace Booking_Airline.Models
{
    public class SeatDetails
    {
        public Guid Id { get; set; }
        public string SeatCode { get; set; } 
        public TravelClass Class { get; set; }
        public Guid FlightId { get; set; }
        public FlightDetail Flight { get; set; }
        public AdditionalSeatService SeatAdditionalService { get; set; }
        public bool IsBooked { get; set; }
    }
}
