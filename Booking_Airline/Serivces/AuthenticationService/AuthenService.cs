using Booking_Airline.DTOs;
using Booking_Airline.DTOs.UserDTOs;
using Booking_Airline.Models;
using Booking_Airline.Models.Exceptions;
using Booking_Airline.REPOSITORYMANAGER;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Booking_Airline.Serivces.AuthenticationService
{
    internal sealed class AuthenService : IAuthenService
    {
        private readonly IRepositoryManager repositoryManager;
        private readonly TokenValidationParameters _CheckRefreshToken;
        private readonly IConfiguration _config;
        private readonly IOptions<JWTConfig> options;
        public AuthenService(IRepositoryManager repo , TokenValidationParameters tokenValidationParameters , IConfiguration config
            , IOptions<JWTConfig> options )
        {
            repositoryManager = repo;
            _CheckRefreshToken = tokenValidationParameters;
            _config=config;
            this.options = options;
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
        public async Task<IActionResult> GetNewAccessToken(IRequestCookieCollection cookies, IResponseCookies resCookies)
        {
            //Refresh Token Rotation
            var refreshToken = cookies["refreshToken"];
            if (refreshToken == null)
            {
                throw new TokenException("Token is not founded"); ;
            }
            var foundedUser = await repositoryManager.UserRepository.GetUserByRefreshToken(refreshToken, false);

            if (foundedUser == null)
            {
                //Có 2 trường hợp :
                //TH1:Token không khớp với token đã được cấp
                //TH2:Token đã được sử dụng(khả năng token bị đánh cắp)
                var principal = GetPrincipalFromExpriedToken(refreshToken);
                if (principal == null)//TH1
                {
                    throw new TokenException("Invalid token");
                }
                //TH2:Delete All Family Refresh Token
                var tokensToUpdate = await repositoryManager.RefreshTokenRepository.GetTokenByUserID(Guid.Parse(principal.FindFirst("UserId").Value), true);
                foreach (var token in  tokensToUpdate)
                {
                    token.IsUsed = true;
                }

                await repositoryManager.SaveAync();
                throw new TokenException("Token is used , Please Login to Get New One");
            }
            else
            {
                //Kiem tra token expired
                var tokenHandler2 = new JwtSecurityTokenHandler();
                var principal2 = tokenHandler2.ValidateToken(refreshToken, _CheckRefreshToken, out SecurityToken SecurityToken);
                if (principal2 == null)
                {
                    throw new TokenException("Invalid token");
                }
                var tokenIsUsed = await repositoryManager.RefreshTokenRepository.GetTokenByRefreshToken(refreshToken,true);
                tokenIsUsed.IsUsed = true;
                await repositoryManager.SaveAync();
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

        public async Task<IActionResult> Login(UserLoginDTO request, IRequestCookieCollection cookies, IResponseCookies resCookies)
        {
            var user = await repositoryManager.UserRepository.GetUserByName(request.Username,false);
            if (user == null)
            {

                throw new UserNotFoundException("User Not Found");
            }
       
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {

                throw new NotVerifiedException("Invalid username or password");

            }
            var refreshToken = cookies["refreshToken"];
            if (refreshToken != null)
            {
                //Check token reuse?
                var foundedUser = await repositoryManager.UserRepository.GetUserByRefreshToken(refreshToken, false);
                if (foundedUser == null)
                {
                    var principal = GetPrincipalFromExpriedToken(refreshToken);
                    //If principal return object ClaimsPrincipal => Token is Used in the part 
                    //Update all family token to INVALID
                  
                    var tokensToUpdate = await repositoryManager.RefreshTokenRepository.GetTokenByUserID(Guid.Parse(principal.FindFirst("UserId").Value), true);
                    foreach (var token in tokensToUpdate)
                    {
                        token.IsUsed = true;
                    }
                    await repositoryManager.SaveAync();
                }
                else
                {   //Update to state token equal "invalid" <=> IsUsed=True
                    var tokenIsUsed = await repositoryManager.RefreshTokenRepository.GetTokenByRefreshToken(refreshToken,true);
                    tokenIsUsed.IsUsed = true;
                    await repositoryManager.SaveAync();
                }
            }
            var RoleNames = await repositoryManager.RoleRepository.GetRoleModelsByUserId(user.Id, false);
            string jwt = CreateToken(user, RoleNames);
            await SetRefreshToken(GenerateRefreshToken(user), resCookies);
            return new OkObjectResult(new
            {
                AccessToken = jwt,
                Message = "Authentication successful"
            });
        }


        public async Task<IActionResult> Logout(IRequestCookieCollection cookies, IResponseCookies resCookies)
        {
            var refreshToken = cookies["refreshToken"];
            if (refreshToken != null)
            {   //Update refresh token TO Invalid State
                var tokenIsUsed = await repositoryManager.RefreshTokenRepository.GetTokenByRefreshToken(refreshToken, true);
                tokenIsUsed.IsUsed = true;
                await repositoryManager.SaveAync();
            }
            //DELETE COOKIES
            resCookies.Delete("refreshToken");
            return new StatusCodeResult(204);
        }

        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            var userExists = await repositoryManager.UserRepository.GetUserByEmail(request.Email,false);
            if (userExists != null)
            {

                throw new Exception("User already exsits");
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, salt);
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashPassword,
                VerifyCode = CreateRandomCode(),
                IsVerified = false,
                Phone = request.Phone

            };
            var userRole = await repositoryManager.RoleRepository.GetRole("User", false);
            if (userRole != null)
            {
                newUser.Roles = new List<RoleModel> { userRole };
            }
            repositoryManager.UserRepository.CreateUser(newUser);
            await repositoryManager.SaveAync();
            var email = new EmailDTO(
                request.Email,
                request.Username,
                newUser.VerifyCode
            );

            repositoryManager.emailRepository.SendEmail(email);
            return new ObjectResult("Successful , please check your email to verify account")
            {
                StatusCode = 200
            };

        }

        public async Task<IActionResult> VerifyAccount(UserVerifyDTO request, IResponseCookies cookies)
        {
            var existsUser = await repositoryManager.UserRepository.GetUserByEmail(request.Email,true);
            if (existsUser == null)
            {
                throw new UserNotFoundException("user not exsits");
            }
            else
            {
                if (existsUser.VerifyCode == request.VerifyToken)
                {
                    existsUser.IsVerified = true;
                    await repositoryManager.SaveAync();
                    var RoleNames = await repositoryManager.RoleRepository.GetRoleModelsByUserId(existsUser.Id, false);
                    string jwt = CreateToken(existsUser, RoleNames);
                    await SetRefreshToken(GenerateRefreshToken(existsUser), cookies);

                    return new OkObjectResult(new
                    {
                        AccessToken = jwt,
                        Message = "Authentication successful"
                    });

                }
                throw new Exception("Verify token failed");
              
            }
        }
        private string CreateRandomCode()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
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
            repositoryManager.RefreshTokenRepository.CreateToken(refreshToken);
            await repositoryManager.SaveAync();
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
    }
}
