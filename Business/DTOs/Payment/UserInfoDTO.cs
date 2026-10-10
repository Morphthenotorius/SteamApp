using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.Payment
{
    public sealed record UserInfoDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}

