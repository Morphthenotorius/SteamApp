using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.LibraryDTO
{
    public sealed record CreateLibraryDTO
    {
        public Guid UserId { get; set; }
    }
}
