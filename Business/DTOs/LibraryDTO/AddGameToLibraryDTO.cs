using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DTOs.LibraryDTO
{
    public sealed record AddGameToLibraryDTO
    {
        public Guid LibraryId {  get; set; }
        public Guid GameId {  get; set; }
    }
}
