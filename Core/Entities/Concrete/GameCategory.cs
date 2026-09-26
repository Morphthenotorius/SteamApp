using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class GameCategory
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }
        public Guid CategoryId {  get; set; }
        public Category Category { get; set; }
    }
}
