using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.FilterDTO
{
    public record GameFilterDTO : FilterRequest
    {
        public Guid? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
