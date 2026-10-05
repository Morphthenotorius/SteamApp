using Business.DTOs.Payment;
using Core.Utilites.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract.Payment
{
    public interface IStripeService
    {
        Task<IResult> DepositBalance(DepositDTO depositDto);
        Task<IResult> ChargeAsync(string stripeToken,decimal amount,string currency = "usd");
    }
}
