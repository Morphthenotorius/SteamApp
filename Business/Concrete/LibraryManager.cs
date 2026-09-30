using Business.Abstract;
using Business.DTOs;
using Business.DTOs.LibraryDTO;
using Core.Entities.Concrete;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LibraryManager : BaseManager<Library,GetLibraryDTO,CreateLibraryDTO,UpdateLibraryDTO>,ILibraryService
    {
        private readonly ILibraryDAL _libraryDal;
        public LibraryManager(ILibraryDAL libraryDAL) : base(
        libraryDAL,

        // 1. CreateDTO -> Library Entity (Kitabxana ilk dəfə yaradılanda)
        static createDTO => new Library
        {
            UserId = createDTO.UserId,
        },

        // 2. Library Entity -> GetLibraryDTO
        MapToGetLibraryDTO,
        

        // 3. UpdateDTO + Existing Entity
        (updateDTO, existingLibrary) =>
        {
            existingLibrary.UserId = updateDTO.UserId;
        }
    )
        {
            _libraryDal = libraryDAL;
        }



        public async Task<IResult> AddGameToLibraryAsync(Guid userId, Guid GameId)
        {
            var library = await _libraryDal.GetLibraryWithGames(userId);
            if(library == null)
            {
                return new ErrorResult("Error! Library was not found!");
            }

            if(!library.LibraryGames.Any(x=> x.GameId == GameId)){
                library.LibraryGames.Add(new LibraryGames
                {
                    LibraryId = library.Id,
                    GameId = GameId,
                    PurchasedDate = DateTime.UtcNow,
                    PlayedTime = TimeSpan.Zero,
                    IsFavorite = false
                });
            }
            await _libraryDal.UpdateAsync(library);
            return new SuccessResult("Payment was successfully completed the game has added to your library!");
        }

        private static GetLibraryDTO MapToGetLibraryDTO(Library entity)
        {
            return new GetLibraryDTO
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Games = entity.LibraryGames.Select(x => new GetLibraryGamesDTO
                {
                    GameId = x.GameId,
                    GameTitle = x.Game.Title,
                    GameCoverImgUrl = x.Game.CoverImageUrl,
                    PurchasedDate = x.PurchasedDate,
                    PlayedTime = x.PlayedTime,
                    IsFavorite = x.IsFavorite
                }).ToList(),
            };
        }

        public async Task<IDataResult<GetLibraryDTO>> GetUserLibraryWithGamesAsync(Guid userId)
        {
            var library = await _libraryDal.GetLibraryWithGames(userId);
            if(library == null)
            {
                return new ErrorDataResult<GetLibraryDTO>(null,"Library was not found :(");
            }

            var dto = new GetLibraryDTO
            {
                Id = library.Id,
                UserId = library.UserId,
                Games = library.LibraryGames.Select(x => new GetLibraryGamesDTO
                {
                    GameId = x.GameId,
                    GameTitle = x.Game.Title,
                    GameCoverImgUrl = x.Game.CoverImageUrl,
                    PurchasedDate = x.PurchasedDate,
                    PlayedTime = x.PlayedTime,
                    IsFavorite = x.IsFavorite

                }).ToList()
            };
            return new SuccessDataResult<GetLibraryDTO>("Library loaded successfully");
        }

        public async Task<IResult> RefundGame(Guid userId, Guid GameId)
        {
            var library =await _libraryDal.GetLibraryWithGames(userId);
            if( library == null)
            {
                return new ErrorResult("Library was not found :(");
            }

            var game =library.LibraryGames.FirstOrDefault(x=> x.GameId==GameId);
            if(game == null)
            {
                return new ErrorResult("Game was not found in your library");
            }

            await _libraryDal.RemoveGameFromLibrary(game);
            await _libraryDal.UpdateAsync(library);
            return new SuccessResult("Game successfully refunded");
        }

        public async Task<IResult> ToggleFavouriteGameAsync(Guid userId, Guid GameId)
        {
            var library = await _libraryDal.GetLibraryWithGames(userId);
            if (library == null)
            {
                return new ErrorResult("Library was not found :(");
            }

            var game = library.LibraryGames.FirstOrDefault(x => x.GameId == GameId);
            if (game == null)
            {
                return new ErrorResult("Game was not found in your library");
            }

            game.IsFavorite = !game.IsFavorite;
            await _libraryDal.UpdateAsync(library);

            return new SuccessResult("Favourite status successfully updated");
        }

        public override async Task<IDataResult<List<GetLibraryDTO>>> GetAllAsync()
        {

        }
    }
}
