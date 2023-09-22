namespace Booking_Airline.Models
{
    public class FoodForFlight
    {
        public Guid FoodId { get; set; }
        public AddionalFoodService FoodService { get; set; }
        public Guid FlightId { get; set; }
        public FlightDetail FlightDetail { get; set; }
    }
}
