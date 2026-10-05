using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.ReviewDTO
{
    public sealed record GetReviewDTO
    {
        public Guid Id { get; set; }
        public Guid UserId {  get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public bool IsRecommended { get; set; }
    }
}
