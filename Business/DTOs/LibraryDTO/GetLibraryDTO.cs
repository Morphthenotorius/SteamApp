using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Business.DTOs.LibraryDTO
{
    public sealed record GetLibraryDTO
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public List<GetLibraryGamesDTO> Games { get; set; } = new();
    }
}
