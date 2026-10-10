using Business.Abstract;
using Business.DTOs.FilterDTO;
using Business.DTOs.GameDTOs;
using Business.Utilities.Helpers;
using Core.Entities.Concrete;
using Core.Utilites.Results.DataResults;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class GameManager : BaseManager<Game,GetGameDTO,CreateGameDTO,UpdateGameDTO>,IGameService
    {
        private readonly IGameDAL gameDAL;
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

                    SalesCount = entity.LibraryGames?.Count ?? 0,
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
            gameDAL = _gameDAL;
        }

        public override async Task<IDataResult<List<GetGameDTO>>> GetAllAsync()
        {
            try
            {
                var games = await gameDAL.GetGamesWithDetailsAsync();
                if (!games.Any() || games == null)
                {
                    return new ErrorDataResult<List<GetGameDTO>>("No games found :(");
                }

                var model = games.Select(_mapToGetDTO).ToList();
                return new SuccessDataResult<List<GetGameDTO>>(model, "Games retrieved successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetGameDTO>>($"There is an error occured during get process: {ex.Message}");
            }
        }

        public override async Task<IDataResult<GetGameDTO>> GetByIdAsync(Guid id)
        {
            try
            {
                var game = await gameDAL.GetGameWithDetailsByIdAsync(id);
                if (game == null)
                {
                    return new ErrorDataResult<GetGameDTO>("No game was found :(");
                }

                var model = _mapToGetDTO(game);
                return new SuccessDataResult<GetGameDTO>(model, "Game retrieved successfully!");
            }

            catch (Exception ex)
            {
                return new ErrorDataResult<GetGameDTO>($"There is an error occured during get process: {ex.Message}");
            }
        }

        public async Task<IDataResult<PagedResult<GetGameDTO>>> GetFilteredAsync(GameFilterDTO dto)
        {
            Expression<Func<Game, bool>> filter = g => (string.IsNullOrEmpty(dto.SearchTerm) || g.Title.Contains(dto.SearchTerm)) &&
                (!dto.CategoryId.HasValue || g.GameCategories.Any(gc => gc.CategoryId == dto.CategoryId)) &&
                (!dto.MinPrice.HasValue || g.Price >= dto.MinPrice) &&
                (!dto.MaxPrice.HasValue || g.Price <= dto.MaxPrice);

            Func<List<GetGameDTO>, IEnumerable<GetGameDTO>> sort = list =>
            {
                switch (dto.Sortby.ToLower())
                {
                    case "price_asc": return list.OrderBy(g => g.Price);
                    case "price_desc": return list.OrderByDescending(g => g.Price);
                    case "top_sellers": return list.OrderByDescending(g => g.SalesCount);
                    case "most_liked": return list.OrderByDescending(g => g.PositivePercentage);
                    default: return list;
                }
            };

            return await GetPagedAsync(filter, q => q
            .Include(g => g.Publisher)
            .Include(g => g.CoverImageUrl)
            .Include(g => g.DevCompany)
            .Include(g => g.GameCategories)
            .ThenInclude(gc => gc.Category)
            .Include(g => g.Reviews)
            .Include(g => g.LibraryGames),
            sort, dto.PageIndex, dto.PageSize);
        
        }
    }
}
