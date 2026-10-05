using Core.Entities.Enums.Payment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.LibraryDTO
{
    public sealed record AddGameToLibraryDTO
    {
        public Guid LibraryId {  get; set; }
        public Guid GameId {  get; set; }
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Wallet;
        public string? StripeToken {  get; set; }
    }
}
