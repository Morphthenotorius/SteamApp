using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.CategoryDTO
{
    public sealed record GetCategoryDTO 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
