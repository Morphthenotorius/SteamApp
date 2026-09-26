using Business.Abstract;
using Business.DTOs.ReviewDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAllAsync();
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
            var result = await _reviewService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }

        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> Create(CreateReviewDTO dto)
        {
            var result = await _reviewService.AddAsync(dto);
            if (result.IsSuccess)
            {
                return Created();
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("UpdateReview")]
        public async Task<IActionResult> Update(UpdateReviewDTO dto, Guid Id)
        {
            var result = await _reviewService.UpdateAsync(dto, Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("DeleteReview")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _reviewService.DeleteAsync(id);
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
