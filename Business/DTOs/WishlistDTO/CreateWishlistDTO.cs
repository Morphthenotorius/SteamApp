using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.WishlistDTO
{
    public sealed record CreateWishlistDTO
    {
        public Guid UserId {  get; set; }
        public Guid GameId { get; set; }
    }
}
