using Business.DTOs;
using Business.DTOs.WishlistDTO;
using Core.Entities.Concrete;
using Core.Utilites.Results;
using Core.Utilites.Results.DataResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IWishlistService : IServiceBase<GetWishlistDTO,CreateWishlistDTO,DummyDTO,Wishlist>
    {
        Task<IResult> RemoveFromWishlistAsync(Guid gameId, Guid userId);
        Task<IDataResult<List<GetWishlistDTO>>> GetWishlistWithGamesAsync(Guid userId);  
    }
}
