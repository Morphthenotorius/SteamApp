using Business.Abstract;
using Business.DTOs.ReviewDTO;
using Core.Entities.Concrete;
using Core.Repository.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ReviewManager : BaseManager<Review,GetReviewDTO,CreateReviewDTO,UpdateReviewDTO>, IReviewService
    {
        public ReviewManager(IReviewDAL reviewDAL) : base(reviewDAL,
            createDto => new Review
            {
                GameId = createDto.GameId,
                UserId = createDto.UserId,
                Content = createDto.Content,
                IsRecommended = createDto.IsRecommended,
            },
            
            static entity => new GetReviewDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Description = entity.Description,
                Content = entity.Content,
                IsRecommended = entity.IsRecommended,
            },

            (updateDto,existingEntity) =>
            {
                existingEntity.Description = updateDto.Description;
                existingEntity.Content = updateDto.Content;
                existingEntity.IsRecommended = updateDto.IsRecommended;
            }
            ) 
        
        
        {
        }  
    }
}
