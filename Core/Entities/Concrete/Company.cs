using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Company : BaseEntity,IEntity
    {
        public string Name { get; set; }
        public List<Game> PublishedGame { get; set; }
        public List<Game> DevGames { get; set; }
        public string? WebsiteUrl {  get; set; }
    }
}
