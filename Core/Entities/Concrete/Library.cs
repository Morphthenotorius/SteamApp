using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Library : BaseEntity,IEntity
    {
        public string Title {  get; set; }
        public string CoverImgUrl {  get; set; }
        public Guid UserId { get; set; }
        public User User {  get; set; }
        public Guid GameId { get; set; }
        public Game Game {  get; set; }
        public List<LibraryGames> LibraryGames { get; set; }
       
    }
}
