using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.WishlistDTO
{
    public sealed record GetWishlistDTO
    {
        public Guid UserId {  get; set; }
        public Guid GameId { get; set; }
        public string Title { get; set; } = string.Empty; // 👈 HƏ, BU MÜTLƏQ OLMALIDIR!
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
    }
}
