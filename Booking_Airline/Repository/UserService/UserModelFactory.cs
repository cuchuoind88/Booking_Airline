using Booking_Airline.DTOs;
using Booking_Airline.Models;

namespace Booking_Airline.Repository.UserService
{
    public class UserModelFactory : IUserModelFactory
    {
        public User Create(UserRegisterDTO request, string hashPassword, string salt, string random)
        {

            return new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashPassword,
                VerifyCode = random,
                IsVerified = false,
                Phone=request.Phone
            };
            
        }
    }
}
