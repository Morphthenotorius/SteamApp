using Business.Abstract;
using Business.DTOs.LibraryDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpPost("BuyGame")]
        public async Task<IActionResult> BuyGame([FromBody] AddGameToLibraryDTO model)
        {
            var result = await _libraryService.BuyGameAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _libraryService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var result = await _libraryService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }

        }

        [HttpPost("RefundGame")]
        public async Task<IActionResult> RefundGame(Guid userId,Guid gameId)
        {
            var result = await _libraryService.RefundGame(userId, gameId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet ("GetUserLibraryWGames")]
        public async Task<IActionResult> GetUserLibraryWithGames(Guid userId)
        {
            var result = await _libraryService.GetUserLibraryWithGamesAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }

        }

        [HttpPost("ToggleFavourite")]
        public async Task<IActionResult> ToggleFavourite(Guid userId,Guid gameId)
        {
            var result = await _libraryService.ToggleFavouriteGameAsync(userId, gameId);
            if(result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("UpdateLibrary")]
        public async Task<IActionResult> Update(UpdateLibraryDTO dto, Guid Id)
        {
            var result = await _libraryService.UpdateAsync(dto, Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("DeleteLibrary")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _libraryService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }
        }
    }
}