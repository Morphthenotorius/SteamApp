using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.GameDTOs
{
    public sealed record CreateGameDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Guid PublisherId { get; set; }
        public Guid DeveloperId { get; set; }
        public List<Guid> CategoryIds {  get; set; }
    }
}
