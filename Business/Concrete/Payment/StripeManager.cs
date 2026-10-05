using Business.Abstract.Auth;
using Business.Abstract.Payment;
using Business.DTOs.Payment;
using Core.Entities.User;
using Core.Utilites.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete.Payment
{
    public class StripeManager : IStripeService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public StripeManager(IConfiguration configuration,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<IResult> ChargeAsync(string stripeToken, decimal amount, string currency = "usd")
        {
            try
            {
                var paymentMethodOptions = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Token = stripeToken,
                    },
                };
                var paymentMethod = await new PaymentMethodService().CreateAsync(paymentMethodOptions);

                var intentOptions = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = currency,
                    PaymentMethod = paymentMethod.Id,
                    Confirm = true,
                    Description = "Gaming Library Purchase",
                    AllowedPaymentMethodTypes = new List<string> { "card" },
                };
                var intent = await new PaymentIntentService().CreateAsync(intentOptions);

                if (intent.Status == "succeeded")
                {
                    return new SuccessResult("Payment has successfully completed");
                }

                return new ErrorResult($"Payment is Unsuccessful (status: {intent.Status})");
            }
            catch(Exception ex)
            {
                return new ErrorResult($"Unknown error happened during payment {ex.Message}");
            }

        }

        public async Task<IResult> DepositBalance(DepositDTO depositDto)
        {
            try
            {
                if (depositDto.Amount <= 0)
                {
                    return new ErrorResult("Amount must be higher than zero");
                }

                if (string.IsNullOrEmpty(depositDto.StripeToken))
                {
                    return new ErrorResult("Stripe token is required for deposit!");
                }

                var user = await _userManager.FindByIdAsync(depositDto.UserId.ToString());
                if (user == null)
                {
                    return new ErrorResult("User was not found");
                }

                var stripeResult = await ChargeAsync(depositDto.StripeToken, depositDto.Amount);
                if (!stripeResult.IsSuccess)
                {
                    return new ErrorResult($"Card payment failed: {stripeResult.Message}");
                }

                user.Balance += depositDto.Amount;
                var updateResult = await _userManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    return new SuccessResult($"{depositDto.Amount} USD was successfully added to your account balance. Current balance {user.Balance}");
                }
                return new ErrorResult("Payment is unsuccessful");
            }

            catch(Exception ex)
            {
                return new ErrorResult($"Unknown error occured during deposit process {ex.Message}");
            }
        }
    }
}
