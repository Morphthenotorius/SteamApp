using Core.Entities.Concrete;
using Core.Repository.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfGameDAL : EfRepositoryBase<Game, AppDbContext>, IGameDAL
    {
        public EfGameDAL(AppDbContext context) : base(context)
        {
        }
    }
}
