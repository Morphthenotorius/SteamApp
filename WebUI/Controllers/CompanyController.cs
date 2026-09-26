using Business.Abstract;
using Business.DTOs.CompanyDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _companyService.GetAllAsync();
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
            var result = await _companyService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return NotFound(result.Message);
            }

        }

        [HttpPost("CreateCompany")]
        public async Task<IActionResult> Create(CreateCompanyDTO dto)
        {
            var result = await _companyService.AddAsync(dto);
            if (result.IsSuccess)
            {
                return Created();
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpPut("UpdateCompany")]
        public async Task<IActionResult> Update(UpdateCompanyDTO dto, Guid Id)
        {
            var result = await _companyService.UpdateAsync(dto, Id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpDelete("DeleteCompany")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _companyService.DeleteAsync(id);
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
