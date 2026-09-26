using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Review : BaseEntity,IEntity
    {
        public string Content {  get; set; }
        public string Description {  get; set; }
        public bool IsRecommended {  get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid GameId {  get; set; }
        public Game Game { get; set; }
    }
}
