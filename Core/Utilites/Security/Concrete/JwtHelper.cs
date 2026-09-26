using Core.Entities.Concrete.Jwt;
using Core.Utilites.Security.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilites.Security.Concrete
{
    public class JwtHelper : ITokenHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using var RandomGenerator = RandomNumberGenerator.Create();

            RandomGenerator.GetBytes(number);

            return Convert.ToBase64String(number);
        }

        public Token CreateToken(List<Claim> claims)
        {
            var secretkey = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(secretkey))
                throw new InvalidOperationException();

            var SignInKey = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(secretkey));

            var AccessTokenExpiration = DateTime.UtcNow.AddMinutes(5);

            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: AccessTokenExpiration,
                signingCredentials: new SigningCredentials(SignInKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = CreateRefreshToken();
            var tokenExpiration = DateTime.UtcNow.AddMonths(2);

            return new Token
            {
                RefreshToken = refreshToken,
                RefreshTokenExpiration = tokenExpiration,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpiration = AccessTokenExpiration
            };



            
        }
    }
}
