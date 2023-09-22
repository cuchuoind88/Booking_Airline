namespace Booking_Airline.Models
{
    public class AddionalFoodService
    {
        public Guid Id { get; set; }
        public string FoodName { get; set; }

        public string FoodDescription { get; set;}
        public decimal FoodPrice { get; set; }
        public ICollection<FlightDetail> flightDetails { get; set; }
        public ICollection<Reservation> reservationDetails { get; set; }
    }
}
