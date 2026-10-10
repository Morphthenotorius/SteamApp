using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.FilterDTO
{
    public record FilterRequest
    {
        public string? SearchTerm {  get; set; }
        public string? Sortby {  get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
