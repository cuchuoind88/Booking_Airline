namespace Booking_Airline.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public ICollection<User> Users { get; set; }
    };
}
