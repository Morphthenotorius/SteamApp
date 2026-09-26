using Business.DTOs.GameDTOs;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IGameService : IServiceBase<GetGameDTO,CreateGameDTO,UpdateGameDTO,Game>
    {
    }
}
