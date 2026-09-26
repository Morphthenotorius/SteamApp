using Business.DTOs.ReviewDTO;
using Core.Entities.Concrete;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IReviewService : IServiceBase<GetReviewDTO,CreateReviewDTO,UpdateReviewDTO,Review>
    {
    }
}
