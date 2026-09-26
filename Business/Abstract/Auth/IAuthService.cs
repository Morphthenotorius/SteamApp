using Business.DTOs.AuthDTO;
using Business.DTOs.AuthDTO.TokenDTO;
using Core.Utilites.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.Auth
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO model);
        Task<TokenDTO> LoginAsync(LoginDTO model);
    }
}
