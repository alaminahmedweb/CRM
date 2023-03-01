using ApplicationCore.Constants;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class TokenClaimService : ITokenClaimService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenClaimService(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<string> GetTokenAsync(string userName)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
                var user = await _userManager.FindByNameAsync(userName);
                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims.ToArray()),
                    Expires = DateTime.UtcNow.AddMinutes(AuthorizationConstants.ExpirationMinutes),
                    Issuer = AuthorizationConstants.JWT_ISSUER,
                    Audience = AuthorizationConstants.JWT_AUDIENCE,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw;
            }
        }
    }
}
