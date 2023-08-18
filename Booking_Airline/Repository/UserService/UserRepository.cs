using Booking_Airline.DTOs;
using Booking_Airline.Models;
using Booking_Airline.Repository.EmailService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking_Airline.Repository.UserService
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _context;
        public IUserModelFactory _userModelFactory;
        private readonly IOptions<JWTConfig> options;
        private readonly IEmailRepository _emailRepro;
        public UserRepository(ApplicationDbContext context, IOptions<JWTConfig> options, IUserModelFactory userModelFactory, IEmailRepository emailRepro)
        {
            _context = context;
            this.options = options;
            _userModelFactory = userModelFactory;
            _emailRepro = emailRepro;

        }

        public Task<IActionResult> Login(UserLoginDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (userExists != null)
            {
                var error = new ErrorModel
                {
                    StatusCode = 400, // Mã lỗi BadRequest,
                    Message = "User already exists."
                };
                return new BadRequestObjectResult(error);
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var newUser = _userModelFactory.Create(request, hashPassword, salt, CreateRandomCode());
            var userRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleName == "User");
            if (userRole != null)
            {
                newUser.Roles = new List<RoleModel> { userRole };
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            var email = new EmailDTO
            {
                To = request.Email,
                Subject = request.Username,
                Random = newUser.VerifyCode
            };
            _emailRepro.SendEmail(email);

            return new ObjectResult("Successful , please check your email to verify account")
            {
                StatusCode = 200
            };

        }

        public async Task<IActionResult> VerifyAccount(UserVerifyDTO request)
        {
            var existsUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existsUser == null)
            {
                var error = new ErrorModel
                {
                    StatusCode = 400, // Mã lỗi BadRequest,
                    Message = "User not exists."
                };
                return new BadRequestObjectResult(error);
            }
            else
            {
                if (existsUser.VerifyCode == request.VerifyToken)
                {
                    existsUser.IsVerified = true;
                    await _context.SaveChangesAsync();
                    var RoleNames =await _context.Roles.Where(role => role.Users.Any(user => user.Id == existsUser.Id)).Select(role => role.RoleName).ToListAsync();
                    string jwt=CreateToken(existsUser,RoleNames);
                    return new OkObjectResult(new {
                        Token = jwt,
                        Message = "Authentication successful"
                    });

                }
                var error = new ErrorModel
                {
                    StatusCode = 400, // Mã lỗi BadRequest,
                    Message = "Verify token failed"
                };
                return new BadRequestObjectResult(error);
            }
        }
        private string CreateRandomCode()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        private string CreateToken (User request , List<string> RoleNames)
        {
            List < Claim > claims= new List<Claim>()
            {
                new Claim(ClaimTypes.Name,request.Username),
                new Claim(ClaimTypes.Email,request.Email)
            };
            foreach(string role in RoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey( Encoding.ASCII.GetBytes(options.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires:DateTime.Now.AddMinutes(30)
                );
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
