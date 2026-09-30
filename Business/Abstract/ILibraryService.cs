using Business.DTOs.LibraryDTO;
using Core.Entities.Concrete;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILibraryService : IServiceBase<GetLibraryDTO,CreateLibraryDTO,UpdateLibraryDTO,Library>
    {
        Task<IDataResult<GetLibraryDTO>> GetUserLibraryWithGamesAsync(Guid userId);
        Task<IResult> AddGameToLibraryAsync(Guid userId, Guid GameId);
        Task<IResult> ToggleFavouriteGameAsync(Guid userId,Guid GameId);
        Task<IResult> RefundGame(Guid userId, Guid GameId);
    }
}
