﻿
using Booking_Airline.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Repository.UserService
{
    public interface IUserRepository
    {
        public Task<IActionResult> Register(UserRegisterDTO request);
        public Task<IActionResult> Login(UserLoginDTO request, IRequestCookieCollection cookies, IResponseCookies resCookies);
        public Task<IActionResult> Logout(IRequestCookieCollection cookies, IResponseCookies resCookies);
        public Task<IActionResult> VerifyAccount(UserVerifyDTO request, IResponseCookies cookies);
        public Task<IActionResult> GetNewAccessToken(IRequestCookieCollection cookies, IResponseCookies resCookies);
    }
}
