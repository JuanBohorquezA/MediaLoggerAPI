using MediaLogger.Domain.DTOs.Security;
using MediaLogger.Domain.Variables;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MediaLogger.Aplication.BL
{
    public class Token
    {
        private readonly string _jwtSecret;
        public Token(IConfiguration configuration)
        {
            _jwtSecret = configuration.GetSection(AppSettings.JwtSecret).Value ?? string.Empty;
        }
        public string GenerateJwtToken(Login login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public bool IsJwtTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromSeconds(30)
            };

            try
            {
                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return true;
            }
            catch (SecurityTokenValidationException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string? GetNameFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var nameClaim = token.Claims.FirstOrDefault()?.Value;
            return nameClaim;
        }
    }
}


