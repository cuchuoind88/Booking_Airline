using Booking_Airline.DTOs;
using Booking_Airline.Models;

namespace Booking_Airline.Repository.UserService
{
    public interface IUserModelFactory
    {
        public User Create(UserRegisterDTO request, string hashPassword, string salt, string random);
    }
}
