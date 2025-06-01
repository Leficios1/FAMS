using FAMS.Core.Interfaces.Repositories;
using FAMS.Core.Interfaces.Services;
using FAMS.Domain.Constants;
using FAMS.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace FAMS.Api.Services
{
    public class AuthService : IAuthService
    {
        IBaseRepository<User> _userRepo;
        IConfiguration _configuration;
        public AuthService(IBaseRepository<User> userRepo, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _configuration = configuration;
        }

        public async Task<IActionResult> LoginAccount(User user)
        {
            IActionResult? result;
            try
            {
                var checkUser = await _userRepo.Get().Include(x=>x.UserPermission).Where(x=>x.Password==user.Password && x.Email.ToLower().Trim()==user.Email.ToLower().Trim()).FirstOrDefaultAsync();

                if (checkUser == null)
                {
                    result = new UnauthorizedObjectResult(EMS.EM78);
                }
                else
                {
                    JwtSecurityToken token = GetToken(checkUser);

                    result = new OkObjectResult(new 
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
     
            }
            return result;
        }

        public JwtSecurityToken GetToken(User user)
        {
            List<Claim> authClaims = new List<Claim>
            {    
                 new Claim(ClaimTypes.Name, user.Name),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.Role, user.PermissionId),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr"));

            var token = new JwtSecurityToken(

                issuer: "http://localhost:5028",
                audience: "http://localhost:5028",
                claims: authClaims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("JWTAuthenticationHIGHsecuredPasswordVVVp1OH7Xzyr");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "http://localhost:5028",
                    ValidAudience = "http://localhost:5028",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claimsIdentity = new ClaimsIdentity(jwtToken.Claims);

                return new ClaimsPrincipal(claimsIdentity);
            }
            catch (Exception ex)
            {
                // Xử lý trường hợp token không hợp lệ
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }
        public async Task<IActionResult> GetUserByToken(string token)
        {
            var principals = DecodeToken(token);
            if (principals == null) return new BadRequestObjectResult(EMS.EM79);

            var emailClaim = principals.FindFirst(ClaimTypes.Email);
            if (emailClaim == null) return new BadRequestObjectResult(EMS.EM80);

            var userEmail = emailClaim.Value;
            var user = await _userRepo.Get().Select(x => new
            {
                x.Id,
                x.Name,
                x.Status,
                x.DateOfBirth,
                x.AvatarUrl,
                x.Gender,
                x.PermissionId,
                x.Email
            }).FirstOrDefaultAsync(x => x.Email.ToLower().Equals(userEmail.ToLower()));

            if (user == null) return new NotFoundObjectResult(EMS.EM81);

            return new OkObjectResult(user);

        }
        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
        }

    }
}
