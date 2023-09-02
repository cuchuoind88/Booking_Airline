namespace Booking_Airline.Models
{
    public class SeatDetails
    {
        public int Id { get; set; }
        public string SeatCode { get; set; } 
        public TravelClass Class { get; set; }
        public int FlightId { get; set; }
        public FlightDetail Flight { get; set; }
        public AdditionalSeatService SeatAdditionalService { get; set; }
        public bool IsBooked { get; set; }
    }
}
