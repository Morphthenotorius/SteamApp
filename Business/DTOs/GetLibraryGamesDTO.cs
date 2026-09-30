using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs
{
    public sealed record GetLibraryGamesDTO
    {
        public Guid GameId { get; set; }
        public string? GameTitle { get; set; }
        public string? GameCoverImgUrl { get; set; }
        public DateTime PurchasedDate { get; set; }
        public TimeSpan PlayedTime { get; set; }
        public bool IsFavorite { get; set; }
    }
}
