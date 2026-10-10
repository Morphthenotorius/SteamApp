using Business.DTOs.FilterDTO;
using Business.DTOs.GameDTOs;
using Core.Entities.Concrete;
using Core.Utilites.Results.DataResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IGameService : IServiceBase<GetGameDTO,CreateGameDTO,UpdateGameDTO,Game>
    {
        Task<IDataResult<PagedResult<GetGameDTO>>> GetFilteredAsync(GameFilterDTO filter);
    }
}
