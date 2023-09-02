namespace Booking_Airline.Models
{
    public class AdditionalSeatService
    {
        public int Id { get; set; }
        public string seatLevel { get; set; }
        public string seatType { get; set; }
        public decimal seatPrice { get; set; }
        public ICollection<SeatDetails> SeatDetails { get; set; }
    }
}
