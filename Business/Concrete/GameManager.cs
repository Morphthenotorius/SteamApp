using Business.Abstract;
using Business.DTOs.GameDTOs;
using Business.Utilities.Helpers;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class GameManager : BaseManager<Game,GetGameDTO,CreateGameDTO,UpdateGameDTO>,IGameService
    {
        public GameManager(IGameDAL _gameDAL) : base(_gameDAL,
            
            createDTO=> new Game
            {
                Title = createDTO.Title,
                Description = createDTO.Description,
                Price = createDTO.Price,
                CoverImageUrl = createDTO.CoverImageUrl,
                DeveloperId = createDTO.DeveloperId,
                PublisherId = createDTO.PublisherId,
                GameCategories= createDTO.CategoryIds.Select(catId=> new GameCategory
                {
                    CategoryId = catId,
                }).ToList(),
            },

            entity =>
            {
                var reviews = entity.Reviews ?? new List<Review>();
                int totalCount = reviews.Count;
                int positiveCount = reviews.Count(r => r.IsRecommended);
                double percentage = 0;
                if (totalCount > 0)
                {
                    percentage = ((double)positiveCount / totalCount) * 100;
                }

                return new GetGameDTO
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    CoverImageUrl = entity.CoverImageUrl,
                    Price = entity.Price,
                    Publisher = entity.Publisher.Name ?? "N/A",
                    DevCompany = entity.DevCompany.Name ?? "N/A",

                    Categories = entity.GameCategories.Select(x => x.Category.Name).ToList(),

                    TotalReviewsCount = totalCount,
                    PositivePercentage = percentage,
                    ReviewSummary = CalculateRating.CalculateGameRating(totalCount, percentage),
                    ReviewComments = reviews.Select(x => x.Content).ToList(),
                };
              
            },
            (updateDTO, existingEntity) =>
            {
                existingEntity.Title = updateDTO.Title;
                existingEntity.Description = updateDTO.Description;
                existingEntity.Price = updateDTO.Price;
                existingEntity.DeveloperId = updateDTO.DeveloperId;
                existingEntity.PublisherId = updateDTO.PublisherId;
                existingEntity.CoverImageUrl = updateDTO.CoverImageUrl;

                existingEntity.GameCategories.Clear();
                if(updateDTO.CategoryIds != null)
                {
                    foreach(var item  in updateDTO.CategoryIds)
                    {
                        existingEntity.GameCategories.Add(new GameCategory
                        {
                            GameId = existingEntity.Id,
                            CategoryId = item
                        });
                    }
                }
            }
            )
        {
            
        }
    }
}
