using Business.Abstract.Auth;
using Business.DTOs.AuthDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
