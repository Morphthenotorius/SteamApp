using Business.Abstract;
using Business.DTOs;
using Business.DTOs.WishlistDTO;
using Core.Entities.Concrete;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Business.Concrete
{
    public class WishlistManager : BaseManager<Wishlist, GetWishlistDTO, CreateWishlistDTO, DummyDTO>, IWishlistService
    {
        private readonly IWishlistDAL _wishlistDal;
        private readonly IGameDAL _gameDal;

        public WishlistManager(IWishlistDAL wishlistDal, IGameDAL gameDal) : base(wishlistDal,
            createDto => new Wishlist
            {
                GameId = createDto.GameId,
                UserId = createDto.UserId
            },

            entity => new GetWishlistDTO
            {
                GameId = entity.GameId,
                Title = entity.Game?.Title,
                CoverImageUrl = entity.Game?.CoverImageUrl,
                Price = entity.Game.Price,
                UserId = entity.UserId,
            },

            (existingEntity, updatedDto) => 
            {
            }


            )
        {
            _gameDal = gameDal;
            _wishlistDal = wishlistDal;
        }

        public override async Task<IResult> AddAsync(CreateWishlistDTO dto)
        {
            try
            {
                var exists = await _wishlistDal.GetAsync(w => w.UserId == dto.UserId);
                if (exists != null)
                {
                    return new ErrorResult("This game already in your wishlist");
                }

                return await base.AddAsync(dto);
            }
            catch (Exception ex)
            {
                return new ErrorResult("An unknown error happened while adding");
            }
        }

        public async Task<IDataResult<List<GetWishlistDTO>>> GetWishlistWithGamesAsync(Guid userId)
        {
            try
            {
                var items = await _wishlistDal.GetWishlistWithGamesAsync(userId);
                if (items == null || !items.Any())
                {
                    return new ErrorDataResult<List<GetWishlistDTO>>("No wishlist was found");
                }
                var result = items.Select(entity => new GetWishlistDTO
                {
                    GameId = entity.GameId,
                    Title = entity.Game?.Title,
                    CoverImageUrl = entity.Game?.CoverImageUrl,
                    Price = entity.Game.Price,
                    UserId = entity.UserId,
                }).ToList();

                return new SuccessDataResult<List<GetWishlistDTO>>(result, "Wishlist retrieved");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetWishlistDTO>>($"An unknown error hapened while retrieve period {ex.Message}");
            }
        }

        public async Task<IResult> RemoveFromWishlistAsync(Guid gameId, Guid userId)
        {
            try
            {
                var game = await _wishlistDal.GetAsync(x => x.GameId == gameId && x.UserId == userId);
                if (game == null)
                {
                    return new ErrorResult("Game was not found in this user's wishlist");
                }

                await _wishlistDal.RemoveAsync(game);
                return new SuccessResult("The game successfully deleted from your wishlist");
            }

            catch (Exception ex)
            {
                return new ErrorResult($"An unknown error hapened during delete period {ex.Message}");

            }
        }
    }
}
