using Core.Entities.Concrete;
using Core.Repository.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class EfReviewDAL : EfRepositoryBase<Review, AppDbContext>, IReviewDAL
    {
        public EfReviewDAL(AppDbContext context) : base(context)
        {
        }
    }
}
