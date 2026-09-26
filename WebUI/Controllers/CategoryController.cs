using Business.Abstract;
using Business.DTOs.CategoryDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
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
            var result = await _categoryService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }
            
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> Create(CreateCategoryDTO dto)
        {
            var result = await _categoryService.AddAsync(dto);
            if (result.IsSuccess)
            {
                return Created();
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> Update(UpdateCategoryDTO dto,Guid Id)
        {
            var result = await _categoryService.UpdateAsync(dto,Id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result =await _categoryService.DeleteAsync(id);
            if(result.IsSuccess)
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
