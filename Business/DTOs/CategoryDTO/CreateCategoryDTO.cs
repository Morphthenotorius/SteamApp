using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.CategoryDTO
{
    public sealed record CreateCategoryDTO
    {
        public string Name { get; set; }
    }
}
