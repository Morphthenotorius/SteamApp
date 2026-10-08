using Business.Abstract;
using Business.DTOs.WishlistDTO;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet("GetWishlist")]
        public async Task<IActionResult> GetWishlist()
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userClaim == null)
            {
                return Unauthorized("User was not found");
            }
            var userId = Guid.Parse(userClaim);
            var result = await _wishlistService.GetWishlistWithGamesAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveFromWishlist(Guid gameId)
        {
            var userClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userClaim == null)
            {
                return Unauthorized("User was not found");
            }

            Guid userId = Guid.Parse(userClaim);
            var result = await _wishlistService.RemoveFromWishlistAsync(gameId, userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("AddGameToWishlist")]
        public async Task<IActionResult> AddToWishlist([FromBody] CreateWishlistDTO createDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("İstifadəçi identifikasiya olunmadı.");
            }

            var userId = Guid.Parse(userIdClaim);
            createDto.UserId = userId;
            var result = await _wishlistService.AddAsync(createDto);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}
