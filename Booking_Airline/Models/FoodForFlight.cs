namespace Booking_Airline.Models
{
    public class FoodForFlight
    {
        public int FoodId { get; set; }
        public AddionalFoodService FoodService { get; set; }
        public int FlightId { get; set; }
        public FlightDetail FlightDetail { get; set; }
    }
}
