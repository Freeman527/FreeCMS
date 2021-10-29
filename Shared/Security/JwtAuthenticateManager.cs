using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;

namespace FreeCMS.Shared.Security
{
    public class JwtAuthenticateManager : IJwtAuthenticationManager
    {
        private readonly IConfiguration _config;

        public JwtAuthenticateManager(IConfiguration config) 
        {
            _config = config;
        }

        public string Authenticate(string username, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_config["JwtConfiguration:TokenKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, username)
                }),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtConfiguration:TokenExpirationMinutes"])),

                Audience = _config["JwtConfiguration:Audience"],

                SigningCredentials = 
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}