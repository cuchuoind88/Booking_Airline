namespace Booking_Airline.Models
{
    public class TicketPrice
    {
        public int TicketPriceId { get; set; }
        public int FlightId { get; set; }
        public FlightDetail FlightDetail { get; set; }
        public int  ClassID { get; set; }
        public TravelClass TravelClass { get; set; }
        public int AvailableSeats { get; set; } // Số lượng vé còn lại
        public decimal Price { get; set; }
    }
}
