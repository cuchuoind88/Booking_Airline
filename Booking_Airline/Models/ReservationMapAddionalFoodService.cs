namespace Booking_Airline.Models
{
    public class ReservationMapAddionalFoodService
    {
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public int NumberOfMeals { get; set; }
        public Guid AdditionalFoodServiceId { get; set; }
        public AddionalFoodService AdditionalFoodService { get; set; }
    }
}
