using Core.Entities.Concrete;
using Core.Repository.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfCategoryDAL : EfRepositoryBase<Category, AppDbContext>, ICategoryDAL
    {
        public EfCategoryDAL(AppDbContext context) : base(context)
        {
        }
    }
}
