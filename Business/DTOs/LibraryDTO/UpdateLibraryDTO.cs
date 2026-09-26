using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.LibraryDTO
{
    public sealed record UpdateLibraryDTO
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
    }
}
