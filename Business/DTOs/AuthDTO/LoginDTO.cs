using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.AuthDTO
{
    public record LoginDTO
    {
        public string EmailOrUsername {  get; set; }
        public string Password { get; set; }
    }
}
