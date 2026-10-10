using Core.Entities.Concrete;
using Core.Repository.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfGameDAL : EfRepositoryBase<Game, AppDbContext>, IGameDAL
    {
        public EfGameDAL(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Game>> GetGamesWithDetailsAsync()
        {
            return await _context.Games
                .Include(g=>g.LibraryGames)
                .Include(g => g.Publisher)
                .Include(g => g.DevCompany)
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Include(g => g.Reviews)
                .ThenInclude(r => r.User)
                .ToListAsync();
        }

        public async Task<Game> GetGameWithDetailsByIdAsync(Guid id)
        {
            return await _context.Games
                .Include(g=>g.LibraryGames)
                .Include(g => g.Publisher)
                .Include(g => g.DevCompany)
                .Include(g => g.GameCategories)
                .ThenInclude(gc => gc.Category)
                .Include(g => g.Reviews)
                .ThenInclude(r => r.User)
                .SingleOrDefaultAsync(g=>g.Id==id);
        }
    }
}
