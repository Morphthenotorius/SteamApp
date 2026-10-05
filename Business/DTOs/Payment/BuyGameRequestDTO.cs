using Core.Entities.Enums.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.Payment
{
    public sealed record BuyGameRequestDTO
    {
        public Guid UserId {  get; set; }
        public Guid GameId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string StripeToken {  get; set; }
    }
}
