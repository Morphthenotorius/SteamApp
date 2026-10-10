using Business.Abstract;
using Business.DTOs.FilterDTO;
using Business.DTOs.GameDTOs;
using Core.Utilites.Results.DataResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result =await _gameService.GetAllAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGameDTO createGameDTO) 
        {
            var result = await _gameService.AddAsync(createGameDTO);
            if(result.IsSuccess)
            {
                return StatusCode(201,result);
            }

            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _gameService.GetByIdAsync(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UpdateGameDTO updateGameDTO,Guid Id)
        {
            var result = await _gameService.UpdateAsync(updateGameDTO, Id);
            if( result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _gameService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> GetFilteredGame([FromQuery]GameFilterDTO dto)
        {
            var result = await _gameService.GetFilteredAsync(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result);
            }
        }
        
    }
}
