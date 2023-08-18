namespace Booking_Airline.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public Reservation ReservationID { get; set; }
        public bool Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
