using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.AuthDTO.TokenDTO
{
    public sealed record TokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
