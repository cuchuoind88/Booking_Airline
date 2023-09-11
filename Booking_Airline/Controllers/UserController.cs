using Azure;
using Booking_Airline.DTOs;
using Booking_Airline.DTOs.UserDTOs;
using Booking_Airline.Models;
using Booking_Airline.Serivces.ServiceManager;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IServiceManger serviceManger;
        public UserController(IServiceManger _service)
        {
            serviceManger = _service;
        }
        [HttpPost]
        [Route("/user/register")]
        public async Task<IActionResult> Register ([FromBody]UserRegisterDTO request)
        {
           return await serviceManger.AuthenService.Register(request);

        }
        [HttpPost]
        [Route("/user/verifyaccount")]
        public async Task<IActionResult> VerifyAccount([FromBody] UserVerifyDTO request)
        {
            
            var result = await serviceManger.AuthenService.VerifyAccount(request , Response.Cookies);
            return result;

        }
        [HttpGet]
        [Route("/user/getNewToken")]
        public async Task<IActionResult> GetNewToken()
        {
            var result = await serviceManger.AuthenService.GetNewAccessToken(Request.Cookies,Response.Cookies);
            return result;
        }
        [HttpPost]
        [Route("/user/login")]
        public async Task<IActionResult> LoginAccount([FromBody] UserLoginDTO request)
        {
            var result = await serviceManger.AuthenService.Login(request, Request.Cookies, Response.Cookies);
            return result;
        }
        [HttpGet]
        [Route("/user/logout")]
        public async Task<IActionResult> LogoutAccount()
        {
            var result = await serviceManger.AuthenService.Logout(Request.Cookies, Response.Cookies);
            return result;
        }
    }
}
