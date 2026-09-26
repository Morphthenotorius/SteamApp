using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Game : BaseEntity,IEntity
    {
        public string Title {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl {  get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid PublisherId { get; set; }
        public Company Publisher {  get; set; }

        public Guid DeveloperId {  get; set; }
        public Company DevCompany { get; set; }
        public List<GameCategory> GameCategories { get; set; }
        public  List<Review> Reviews { get; set; }
        public List<LibraryGames> LibraryGames { get; set; }
    }
}
