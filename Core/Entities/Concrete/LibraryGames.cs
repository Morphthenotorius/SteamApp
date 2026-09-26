using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class LibraryGames 
    {
        public Guid LibraryId {  get; set; }
        public Library Library { get; set; }
        public DateTime PurchasedDate { get; set; }
        public TimeSpan PlayedTime { get; set; }
        public bool IsFavorite { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }



    }
}
