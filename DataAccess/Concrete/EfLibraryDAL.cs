using Core.Entities.Concrete;
using Core.Repository.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfLibraryDAL : EfRepositoryBase<Library, AppDbContext>, ILibraryDAL
    {
        public EfLibraryDAL(AppDbContext context) : base(context)
        {
        }

        public async Task<Library> GetLibraryWithGames(Guid id)
        {
            return await _context.Libraries
                .Include(l => l.LibraryGames)
                .ThenInclude(lg => lg.Game)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }
        

        public async Task<List<Library>> GetLibrariesWithGames()
        {
          return await _context.Libraries
          .Include(l => l.LibraryGames)
          .ThenInclude(lg => lg.Game)
          .ToListAsync();
        }
        public async Task RemoveGameFromLibrary(LibraryGames game)
        {
            _context.Set<LibraryGames>().Remove(game);
           await _context.SaveChangesAsync();
        }
    }
}
