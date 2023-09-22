namespace Booking_Airline.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Passenger PassengerID { get; set; }
        public SeatDetails SeatDetailsID { get; set; }
        public DateTime DateOfReservation { get; set; }
        public string RervationCode { get; set; }
        public ICollection<AddionalFoodService> AddionalFoodServices { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
