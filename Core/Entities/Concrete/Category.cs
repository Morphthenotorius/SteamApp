using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Category : BaseEntity,IEntity
    {
        public string Name { get; set; }
        public List<GameCategory> GameCategories { get; set; }
    }
}
