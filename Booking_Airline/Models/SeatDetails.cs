namespace Booking_Airline.Models
{
    public class SeatDetails
    {
        public int Id { get; set; }
        public decimal TicketPrice { get; set; }
        public TravelClass Class { get;set; }
        public FlightDetail Filght { get; set; }
    }
}
