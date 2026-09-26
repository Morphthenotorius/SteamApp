using Core.Entities.Concrete.Jwt;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Core.Utilites.Security.Abstract
{
    public interface ITokenHelper
    {
        public Token CreateToken(List<Claim> claims);
        public string CreateRefreshToken();
    }
}
