namespace Booking_Airline.Models
{
    public class ReservationMapAddionalFoodService
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int NumberOfMeals { get; set; }
        public int AdditionalFoodServiceId { get; set; }
        public AddionalFoodService AdditionalFoodService { get; set; }
    }
}
