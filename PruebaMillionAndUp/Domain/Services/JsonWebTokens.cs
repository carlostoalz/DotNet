using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Domain.Services
{
    public static class JsonWebTokens
    {
        public static string Sing(int idOwner, string key)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
            SecurityTokenDescriptor jwt = new()
            {
                Subject = new(new Claim[] { 
                    new(ClaimTypes.Name, idOwner.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new();
            var token = handler.CreateToken(jwt);
            return handler.WriteToken(token);
        }
    }
}
