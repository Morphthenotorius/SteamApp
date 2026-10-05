using Business.Abstract.Payment;
using Business.DTOs.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Payment
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IStripeService _stripeService;

        public PaymentController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositDTO dto)
        {
            var result = await _stripeService.DepositBalance(dto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
