using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.GameDTOs
{
    public sealed record GetGameDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Publisher { get; set; }
        public string DevCompany { get; set; }
        public List<string> Categories { get; set; }
        
        public int TotalReviewsCount {  get; set; }
        public double PositivePercentage {  get; set; }
        public string ReviewSummary {  get; set; }
        public List<string> ReviewComments { get; set; }
    }
}
