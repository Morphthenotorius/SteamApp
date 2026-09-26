using Business.Abstract;
using Business.DTOs.LibraryDTO;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LibraryManager : BaseManager<Library,GetLibraryDTO,CreateLibraryDTO,UpdateLibraryDTO>,ILibraryService
    {
        public LibraryManager(ILibraryDAL libraryDAL) : base(
        libraryDAL,

        // 1. CreateDTO -> Library Entity (Kitabxana ilk dəfə yaradılanda)
        static createDTO => new Library
        {
            UserId = createDTO.UserId,
            GameId = createDTO.GameId,
        },

        // 2. Library Entity -> GetLibraryDTO
        static entity => new GetLibraryDTO
        {
            UserId = entity.UserId,
            // Sənin GetLibraryDTO-da olan LibraryGames siyahısı
            Games = entity.LibraryGames ?? new List<LibraryGames>()
        },

        // 3. UpdateDTO + Existing Entity
        (updateDTO, existingLibrary) =>
        {
            existingLibrary.UserId = updateDTO.UserId;
        }
    )
        {
        }
    }
}
