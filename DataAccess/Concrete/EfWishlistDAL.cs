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
    public class EfWishlistDAL : EfRepositoryBase<Wishlist, AppDbContext>, IWishlistDAL
    {
        public EfWishlistDAL(AppDbContext context) : base(context)
        {
        }

        public Task<List<Wishlist>> GetWishlistWithGamesAsync(Guid userId)
        {
            return _context.Wishlist
                .Where(x => x.UserId == userId)
                .Include(w => w.Game)
                .ToListAsync();
        }
    }
}
