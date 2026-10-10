using Business.Abstract;
using Business.Abstract.Payment;
using Business.DTOs;
using Business.DTOs.LibraryDTO;
using Core.Entities.Concrete;
using Core.Entities.Enums.Payment;
using Core.Entities.User;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LibraryManager : BaseManager<Library,GetLibraryDTO,CreateLibraryDTO,UpdateLibraryDTO>,ILibraryService
    {
        private readonly IStripeService _stripeService;
        private readonly ILibraryDAL _libraryDal;
        private readonly AppDbContext _dbContext;
        private readonly IGameDAL _gamedal;
        private readonly UserManager<AppUser> _userManager;

        public LibraryManager(ILibraryDAL libraryDAL,AppDbContext dbContext,IGameDAL gameDAL,UserManager<AppUser> userManager,IStripeService stripeService) : base(
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
            _stripeService = stripeService;
            _gamedal = gameDAL;
            _dbContext = dbContext;
            _libraryDal = libraryDAL;
            _userManager = userManager;
        }

        public async Task<IResult> BuyGameAsync(AddGameToLibraryDTO dto)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var library = (await _libraryDal.GetAsync(l => l.Id == dto.LibraryId,
                 include: l => l.Include(x => x.LibraryGames))).FirstOrDefault();
                if (library == null)
                {
                    return new ErrorResult("Library was not found :(");
                }
                bool isAlreadyOwned = library.LibraryGames.Any(lg => lg.GameId == dto.GameId);
                if (isAlreadyOwned)
                {
                    return new ErrorResult("You already own this game");
                }

                var game =await _gamedal.GetGameWithDetailsByIdAsync(dto.GameId);
                if(game == null)
                {
                    return new ErrorResult("Game was not found");
                }

                var user = await _userManager.FindByIdAsync(library.UserId.ToString());
                if (user == null)
                {
                    return new ErrorResult("User was not found");
                }

                decimal gamePrice = game.Price;
                decimal amountToChargeCard = 0;

                if (dto.PaymentMethod == PaymentMethod.Wallet)
                {
                    if (user.Balance < game.Price)
                    {
                        return new ErrorResult("Insufficent Balance");
                    }

                    user.Balance -= game.Price;
                    await _userManager.UpdateAsync(user);
                }

                else if (dto.PaymentMethod == PaymentMethod.CreditCard)
                {
                    if (string.IsNullOrEmpty(dto.StripeToken))
                    {
                        return new ErrorResult("Stripe token is required for credit card payment!");    
                    }

                    var stripeResult = await _stripeService.ChargeAsync(dto.StripeToken, game.Price);
                    if (!stripeResult.IsSuccess)
                    {
                        return new ErrorResult($"Card payment failed: {stripeResult.Message}");
                    }
                }

                library.LibraryGames.Add(new LibraryGames
                {
                    LibraryId = dto.LibraryId,
                    GameId = dto.GameId,
                    PurchasedDate = DateTime.UtcNow,
                    PlayedTime = TimeSpan.Zero,
                    IsFavorite = false,

                });
                await _libraryDal.UpdateAsync(library);

                await transaction.CommitAsync();

                return new SuccessResult($"Purchase accomplished {game.Title} was successfully added to your library");
            }

            catch(Exception ex) 
            {
                await transaction.RollbackAsync();
                return new ErrorResult($"Unknown error occured during purchase process {ex.Message}");
            }
        }

        public async Task<IResult> AddGameToLibraryAsync(Guid userId, Guid GameId)
        {
            var library = await _libraryDal.GetLibraryWithGames(userId);
            if(library == null)
            {
                return new ErrorResult("Error! Library was not found!");
            }

            if(!library.LibraryGames.Any(x=> x.GameId == GameId))
            {
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
            try
            {
                var library = await _libraryDal.GetLibraryWithGames(userId);
                if (library == null)
                {
                    return new ErrorDataResult<GetLibraryDTO>(null, "Library was not found :(");
                }

                var dto = MapToGetLibraryDTO(library);
                return new SuccessDataResult<GetLibraryDTO>(dto,"Library loaded successfully");
            }
            
            catch (Exception ex)
            {
                return new ErrorDataResult<GetLibraryDTO>($"There is an error occured during get process: {ex.Message}");
            }
        }

        public async Task<IResult> RefundGame(Guid userId, Guid GameId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var library = await _libraryDal.GetLibraryWithGames(userId);
                if (library == null)
                    return new ErrorResult("Library was not found :(");

                var owned = library.LibraryGames.FirstOrDefault(x => x.GameId == GameId);
                if (owned == null)
                    return new ErrorResult("Game was not found in your library");

                if (DateTime.UtcNow - owned.PurchasedDate > TimeSpan.FromDays(14))
                    return new ErrorResult("Refund period (14 days) has expired");

                if (owned.PlayedTime > TimeSpan.FromHours(2))
                    return new ErrorResult("You have played more than 2 hours, refund is not available");

                var game = await _gamedal.GetGameWithDetailsByIdAsync(GameId);
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (game == null || user == null)
                    return new ErrorResult("Game or user was not found");

                await _libraryDal.RemoveGameFromLibrary(owned);
                await _libraryDal.UpdateAsync(library);

                user.Balance += game.Price;
                await _userManager.UpdateAsync(user);

                await transaction.CommitAsync();
                return new SuccessResult($"{game.Title} was refunded, {game.Price} returned to your balance");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ErrorResult($"There is an error occured during refund process: {ex.Message}");
            }
        }

        public async Task<IResult> ToggleFavouriteGameAsync(Guid userId, Guid GameId)
        {
            try
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

            catch (Exception ex)
            {
                return new ErrorResult($"There is an error occured during get process: {ex.Message}");
            }
        }

        public override async Task<IDataResult<List<GetLibraryDTO>>> GetAllAsync()
        {
            try
            {

                var libraries = await _libraryDal.GetLibrariesWithGames(); 
                if (libraries == null || !libraries.Any())
                {
                    return new ErrorDataResult <List<GetLibraryDTO>>("Library was not found :(");
                }

                var model =libraries.Select(MapToGetLibraryDTO).ToList();
                return new SuccessDataResult<List<GetLibraryDTO>>(model, "Libraries retrieved");
            }

            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetLibraryDTO>>($"There is an error occured during get process: {ex.Message}");
            }
        }
    }
}
