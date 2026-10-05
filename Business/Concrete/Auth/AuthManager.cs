using Business.Abstract.Auth;
using Business.DTOs.AuthDTO;
using Business.DTOs.AuthDTO.TokenDTO;
using Core.Entities.Concrete;
using Core.Entities.User;
using Core.Utilites.Results;
using Core.Utilites.Security.Abstract;
using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Concrete.Auth
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILibraryDAL _libraryDAL;
        public AuthManager(UserManager<AppUser> userManager, ITokenHelper tokenHelper, ILibraryDAL libraryDAL)
        {
            _libraryDAL = libraryDAL;
            _userManager = userManager;
            _tokenHelper = tokenHelper;

        }

        private readonly ITokenHelper _tokenHelper;
        public async Task<TokenDTO> LoginAsync(LoginDTO model)
        {
            var findUser = await _userManager.FindByEmailAsync(model.EmailOrUsername);
            if (findUser == null)
            {
                findUser = await _userManager.FindByNameAsync(model.EmailOrUsername);
            }

            if (findUser == null)
            {
                return default;
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(findUser, model.Password);
            if (!isPasswordValid)
                return default;

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, findUser.Id.ToString()),
                new(ClaimTypes.Email, findUser.Email ?? ""),
                new(ClaimTypes.Name, findUser.UserName ?? ""),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = _tokenHelper.CreateToken(claims);

            findUser.RefreshToken = token.RefreshToken;
            findUser.RefreshTokenExpiration = token.RefreshTokenExpiration;
            await _userManager.UpdateAsync(findUser);

            return new TokenDTO
            {
                AccessToken = token.AccessToken,
                AccessTokenExpiration = token.AccessTokenExpiration,
                RefreshToken = token.RefreshToken,
                RefreshTokenExpiration = token.RefreshTokenExpiration
            };
        }
        

        public async Task<IResult> RegisterAsync(RegisterDTO model)
        {
            AppUser appUser = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username,
                Balance = 0
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (!result.Succeeded)
            {
                var errorMessage = string.Join(" | ", result.Errors.Select(e => e.Description));
                return new ErrorResult(errorMessage);
            }

            using AppDbContext context = new AppDbContext();
            context.SaveChangesAsync();

            var UserLibrary = new Library
            {
                Id = Guid.NewGuid(),
                UserId = appUser.Id
            };

            await _libraryDAL.AddAsync(UserLibrary);
            return new SuccessResult("Account has successfully created");
        }
    }
}
