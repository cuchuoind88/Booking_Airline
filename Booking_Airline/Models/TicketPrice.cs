namespace Booking_Airline.Models
{
    public class TicketPrice
    {
        public Guid TicketPriceId { get; set; }
        public Guid FlightId { get; set; }
        public FlightDetail FlightDetail { get; set; }
        public Guid ClassID { get; set; }
        public TravelClass TravelClass { get; set; }
        public int AvailableSeats { get; set; } // Số lượng vé còn lại
        public decimal Price { get; set; }
    }
}
