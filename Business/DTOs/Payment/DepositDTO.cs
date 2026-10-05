using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.Payment
{
    public sealed record DepositDTO
    {
        public Guid UserId {  get; set; }
        public decimal Amount {  get; set; }
        public string? StripeToken {  get; set; }
    }
}
