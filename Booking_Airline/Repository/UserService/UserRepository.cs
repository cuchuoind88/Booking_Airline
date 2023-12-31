﻿using Azure;
using Azure.Core;
using Booking_Airline.DTOs;
using Booking_Airline.Models;
using Booking_Airline.Repository.EmailService;
using Booking_Airline.Repository.ErrorService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using System.Diagnostics;
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
        public IErrorHandling _errorHandling;
        private readonly IOptions<JWTConfig> options;
        public readonly IConfiguration _config;
        private readonly IEmailRepository _emailRepro;
        private readonly TokenValidationParameters _CheckRefreshToken;

        public UserRepository(ApplicationDbContext context, IOptions<JWTConfig> options, IUserModelFactory userModelFactory, IEmailRepository emailRepro, IConfiguration config,
            TokenValidationParameters CheckRefreshToken,IErrorHandling errorHandling)
        {
            _context = context;
            this.options = options;
            _userModelFactory = userModelFactory;
            _emailRepro = emailRepro;
            _config = config;
            _CheckRefreshToken = CheckRefreshToken;
            _errorHandling = errorHandling;
        }

        public async Task<IActionResult> Login(UserLoginDTO request, IRequestCookieCollection cookies, IResponseCookies resCookies)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == request.Username);
            if (user == null)
            {
                
                return _errorHandling.GetBadRequestResult("User not found", 401);
            }
            if (user.IsVerified == false)
            {
                return _errorHandling.GetBadRequestResult("User not verified", 401);

            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {

                return _errorHandling.GetBadRequestResult("Invalid Acoount Or Password", 401);
           
            }
            var refreshToken = cookies["refreshToken"];
            if (refreshToken != null)
            {
                //Check token reuse?
                var foundedUser = await _context.Users.Where(user => user.refreshTokens.Any(rf => rf.Token == refreshToken && rf.IsUsed == false)).FirstOrDefaultAsync();
                if (foundedUser == null)
                {
                    var principal = GetPrincipalFromExpriedToken(refreshToken);
                    //If principal return object ClaimsPrincipal => Token is Used in the part 
                    //Update all family token to INVALID
                    var tokensToUpdate = _context.RefreshTokens.Where(t => t.UserId == int.Parse(principal.FindFirst("UserId").Value));
                    foreach (var token in tokensToUpdate)
                    {
                        token.IsUsed = true;
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {   //Update to state token equal "invalid" <=> IsUsed=True
                    var tokenIsUsed = await _context.RefreshTokens.FirstOrDefaultAsync(rf => rf.Token == refreshToken);
                    tokenIsUsed.IsUsed = true;
                    await _context.SaveChangesAsync();
                }
            }
            var RoleNames = await _context.Roles.Where(role => role.Users.Any(user => user.Id == user.Id)).Select(role => role.RoleName).ToListAsync();
            string jwt = CreateToken(user, RoleNames);
            await SetRefreshToken(GenerateRefreshToken(user), resCookies);
            return new OkObjectResult(new
            {
                AccessToken = jwt,
                Message = "Authentication successful"
            });
        }

        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Email == request.Email);
            if (userExists != null)
            {
              
                return _errorHandling.GetBadRequestResult("User already exists.", 401);
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

        public async Task<IActionResult> VerifyAccount(UserVerifyDTO request, IResponseCookies cookies)
        {
            var existsUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existsUser == null)
            {
              
                return _errorHandling.GetBadRequestResult("User not exists.", 400);
            }
            else
            {
                if (existsUser.VerifyCode == request.VerifyToken)
                {
                    existsUser.IsVerified = true;
                    await _context.SaveChangesAsync();
                    var RoleNames = await _context.Roles.Where(role => role.Users.Any(user => user.Id == existsUser.Id)).Select(role => role.RoleName).ToListAsync();
                    string jwt = CreateToken(existsUser, RoleNames);
                    await SetRefreshToken(GenerateRefreshToken(existsUser), cookies);

                    return new OkObjectResult(new
                    {
                        AccessToken = jwt,
                        Message = "Authentication successful"
                    });

                }
                var error = new ErrorModel
                {
                    StatusCode = 400, // Mã lỗi BadRequest,
                    Message = "Verify token failed"
                };
                return _errorHandling.GetBadRequestResult("Verify token failed", 400);
            }
        }
        private string CreateRandomCode()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }
        private string CreateToken(User request, List<string> RoleNames)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,request.Username),
                new Claim(ClaimTypes.Email,request.Email),
            };
            foreach (string role in RoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Value.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(5),
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"]

                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private RefreshToken GenerateRefreshToken(User request)
        {
            List<Claim> claims = new List<Claim>()
            {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim("UserId", request.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:RefreshKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(6),
                signingCredentials: creds,
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"]
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return new RefreshToken()
            {
                Token = jwt,
                UserId = request.Id,
                IsUsed = false
            };
        }
        private async Task SetRefreshToken(RefreshToken refreshToken, IResponseCookies cookies)
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

        public async Task<IActionResult> GetNewAccessToken(IRequestCookieCollection cookies, IResponseCookies resCookies)
        {   //Refresh Token Rotation
            var refreshToken = cookies["refreshToken"];
            if (refreshToken == null)
            {
                return _errorHandling.GetBadRequestResult("Verify token failed", 401);
            }
            var foundedUser = await _context.Users.Where(user => user.refreshTokens.Any(rf => rf.Token == refreshToken && rf.IsUsed == false)).FirstOrDefaultAsync();
            if (foundedUser == null)
            {
                //Có 2 trường hợp :
                //TH1:Token không khớp với token đã được cấp
                //TH2:Token đã được sử dụng(khả năng token bị đánh cắp)
                var principal = GetPrincipalFromExpriedToken(refreshToken);
                if (principal == null)//TH1
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
                var tokenHandler2 = new JwtSecurityTokenHandler();
                var principal2 = tokenHandler2.ValidateToken(refreshToken, _CheckRefreshToken, out SecurityToken SecurityToken);
                if (principal2 == null)
                {
                    return new BadRequestObjectResult("Invalid token");
                }
                var tokenIsUsed = await _context.RefreshTokens.FirstOrDefaultAsync(rf => rf.Token == refreshToken);
                tokenIsUsed.IsUsed = true;
                await _context.SaveChangesAsync();
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(-1), // Đặt thời gian hết hạn trong quá khứ
                    Secure = false // Only for local
                };
                resCookies.Append("refreshToken", "", cookieOptions);
                var roleClaims = principal2.FindAll(ClaimTypes.Role).ToList();
                var RoleNames = roleClaims.Select(claim => claim.Value).ToList();
                var newAccessToken = CreateToken(foundedUser, RoleNames);
                await SetRefreshToken(GenerateRefreshToken(foundedUser), resCookies);
                return new OkObjectResult(new
                {
                    AccessToken = newAccessToken,
                    Message = "Issued New AccessToken successful"
                });

            }

        }
        private ClaimsPrincipal? GetPrincipalFromExpriedToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:RefreshKey"])),
                ValidateLifetime = false//Disable LifeTime-Accept expried token for check reuse
            };
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return principal;
        }

        public async Task<IActionResult> Logout(IRequestCookieCollection cookies, IResponseCookies resCookies)
        {
            var refreshToken = cookies["refreshToken"];
            if (refreshToken != null)
            {   //Update refresh token TO Invalid State
                var tokenIsUsed = await _context.RefreshTokens.FirstOrDefaultAsync(rf => rf.Token == refreshToken);
                tokenIsUsed.IsUsed = true;
                await _context.SaveChangesAsync();
            }
            //DELETE COOKIES
            resCookies.Delete("refreshToken");
            return new StatusCodeResult(204);
        }
    }
    } 
    

