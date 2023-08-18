using Booking_Airline.DTOs;
using Booking_Airline.Models;
using Booking_Airline.Repository.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Airline.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost]
        [Route("/user/register")]
        public async Task<IActionResult> Register ([FromBody]UserRegisterDTO request)
        {
            if (request is null)
            {
                var error = new ErrorModel
                {
                    StatusCode = 400, // Mã lỗi BadRequest,
                    Message = "No content"
                };
                return new BadRequestObjectResult(error);
            }
            var result=await userRepository.Register(request);
            return result;

        }
        [HttpPost]
        [Route("/user/verifyaccount")]
        public async Task<IActionResult> VerifyAccount([FromBody] UserVerifyDTO request)
        {
            if (request is null)
            {
                var error = new ErrorModel
                {
                    StatusCode = 400, // Mã lỗi BadRequest,
                    Message = "No content"
                };
                return new BadRequestObjectResult(error);
            }
            var result = await userRepository.VerifyAccount(request);
            return result;

        }

    }
}
