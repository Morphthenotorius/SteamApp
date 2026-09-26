using Business.Abstract;
using Business.DTOs.LibraryDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
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

        [HttpPost("CreateLibrary")]
        public async Task<IActionResult> Create(CreateLibraryDTO dto)
        {
            var result = await _libraryService.AddAsync(dto);
            if (result.IsSuccess)
            {
                return Created();
            }

            else
            {
                return BadRequest(result.Message);
            }
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