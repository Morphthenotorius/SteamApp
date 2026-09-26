using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete.Jwt
{
    public class Token 
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
