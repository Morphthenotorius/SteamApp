using Core.Entities.Concrete;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ILibraryDAL : IRepositoryBase<Library>
    {
        Task RemoveGameFromLibrary(LibraryGames game);
        Task<Library> GetLibraryWithGames(Guid id);
        Task<List<Library>> GetLibrariesWithGames();
    }
}
