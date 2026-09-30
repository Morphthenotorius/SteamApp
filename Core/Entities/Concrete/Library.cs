using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Library : BaseEntity,IEntity
    {
        public Guid UserId { get; set; }
        public List<LibraryGames> LibraryGames { get; set; }
       
    }
}
