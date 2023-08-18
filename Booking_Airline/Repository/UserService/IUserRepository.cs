
using Booking_Airline.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.UserService
{
    public interface IUserRepository
    {
        public Task<IActionResult> Register(UserRegisterDTO request);
        public Task<IActionResult> Login(UserLoginDTO request);
        public Task<IActionResult> VerifyAccount(UserVerifyDTO request);
    }
}
