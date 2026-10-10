using Business.Abstract.Auth;
using Business.DTOs.AuthDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebUI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginDTO model)
        {
            var result = await _authService.LoginAsync(model);
            if(result!= null)
            {
                return Ok(result);
            }

            else
            {
                return Unauthorized("Email/Username or Password is incorrect");
            }
        }

        [HttpGet("UserInfo")]
        public async Task<IActionResult> UserInfo()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(id))
            {
                return Unauthorized();
            }
            var result = await _authService.GetUserAsync(Guid.Parse(id));
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound(result);
        }

    }
}
