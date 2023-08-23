using Azure;
using Azure.Core;
using Booking_Airline.DTOs;
using Booking_Airline.Models;
using Booking_Airline.Repository.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking_Airline.Repository.UserService
{
    public class UserRepository : IUserRepository
    {
        public ApplicationDbContext _context;
        public IUserModelFactory _userModelFactory;
        private readonly IOptions<JWTConfig> options;
        public readonly IConfiguration _config;
        private readonly IEmailRepository _emailRepro;
        private readonly TokenValidationParameters _CheckRefreshToken;
       
        
        public UserRepository(ApplicationDbContext context, IOptions<JWTConfig> options, IUserModelFactory userModelFactory, IEmailRepository emailRepro, IConfiguration config,
            TokenValidationParameters CheckRefreshToken)
        {
            _context = context;
            this.options = options;
            _userModelFactory = userModelFactory;
            _emailRepro = emailRepro;
            _config=config;
            _CheckRefreshToken = CheckRefreshToken;
            
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

        public async Task<IActionResult> VerifyAccount(UserVerifyDTO request,IResponseCookies cookies)
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
                    await SetRefreshToken(GenerateRefreshToken(existsUser),cookies);

                    return new OkObjectResult(new {
                        AccessToken = jwt,
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
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }
        private string CreateToken (User request , List<string> RoleNames)
        {
            List < Claim > claims= new List<Claim>()
            {
                new Claim(ClaimTypes.Name,request.Username),
                new Claim(ClaimTypes.Email,request.Email),
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
                expires:DateTime.UtcNow.AddMinutes(5),
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"]
               
                );
            var jwt=new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private RefreshToken GenerateRefreshToken (User request)
        {
            List<Claim> claims = new List<Claim>()
            {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim("UserId", request.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:RefreshKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow, // Thêm thông tin thời gian tạo token
                Expires = DateTime.UtcNow.AddSeconds(15),
                SigningCredentials = creds,
                Issuer = _config["JWT:ValidIssuer"],
                Audience = _config["JWT:ValidAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return new RefreshToken()
            {
                Token = jwt,
                UserId = request.Id,
                IsUsed = false
            };
        }
        private async Task SetRefreshToken(RefreshToken refreshToken , IResponseCookies cookies)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(7),
                Secure = false//Only for local 
            };
            cookies.Append("refreshtoken", refreshToken.Token, cookieOptions);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> GetNewAccessToken(IRequestCookieCollection cookies , IResponseCookies resCookies)
        {   //Refresh Token Rotation
            var refreshToken = cookies["refreshToken"];
            if (refreshToken == null)
            {
                var error = new ErrorModel
                {
                    StatusCode = 403, // Mã lỗi BadRequest,
                    Message = "Verify token failed"
                };
                return new BadRequestObjectResult(error);
            }
            var foundedUser = await _context.Users.Where(user => user.refreshTokens.Any(rf => rf.Token == refreshToken && rf.IsUsed == false)).FirstOrDefaultAsync();
            var principal = GetPrincipalFromToken(refreshToken);
            if (foundedUser == null)
            {
                //Có 2 trường hợp :
                //TH1:Token không khớp với token đã được cấp
                //TH2:Token đã được sử dụng(khả năng token bị đánh cắp)
               
                if (principal==null)//TH1
                {
                    return new BadRequestObjectResult("Invalid token");
                }
                //TH2:Delete All Family Refresh Token
                var tokensToUpdate = _context.RefreshTokens.Where(t => t.UserId == int.Parse(principal.FindFirst("UserId").Value));

                foreach (var token in tokensToUpdate)
                {
                    token.IsUsed = true;
                }

                await _context.SaveChangesAsync();
                return new BadRequestObjectResult("Token is used , Please Login to Get New One");
            }
            else
            {
                //Kiem tra token expired
             
               
                if (principal==null)
                {
                    return new BadRequestObjectResult("Token is expired");
                }
                var tokenIsUsed = await _context.RefreshTokens.FirstOrDefaultAsync(rf => rf.Token == refreshToken);
                tokenIsUsed.IsUsed= true;
                await _context.SaveChangesAsync();
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(-1), // Đặt thời gian hết hạn trong quá khứ
                    Secure = false // Only for local
                };
                resCookies.Append("refreshToken", "", cookieOptions);
                var roleClaims = principal.FindAll(ClaimTypes.Role).ToList();
                var RoleNames= roleClaims.Select(claim => claim.Value).ToList();
                var newAccessToken = CreateToken(foundedUser, RoleNames);
                await SetRefreshToken(GenerateRefreshToken(foundedUser),resCookies);
                return new OkObjectResult(new
                {
                    AccessToken = newAccessToken,
                    Message = "Issued New AccessToken successful"
                });

            }

        }
        private ClaimsPrincipal? GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, _CheckRefreshToken, out SecurityToken validatedToken);
            return principal;
        }
    }
}
