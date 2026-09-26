using Business.DTOs.AuthDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Business.Validations.FluentValidation.AuthValidator
{
    public sealed class RegisterDtoValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty!")
            .MaximumLength(50).WithMessage("First name must be 50 symbols maximum!")
            .Matches(@"^[^\d]+$").WithMessage("You cannot use digits in first name.");

            RuleFor(x => x.LastName)
             .NotEmpty().WithMessage("Last name cannot be empty!")
             .MaximumLength(50).WithMessage("Last name must be 50 symbols maximum!")
             .Matches(@"^[^\d]+$").WithMessage("You cannot use digits in last name.");

            RuleFor(x => x.Username)
             .NotEmpty().WithMessage("Username cannot be empty!")
             .MaximumLength(30).WithMessage("Username must be 30 symbols maximum!");

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email cannot be empty!")
             .MaximumLength(50).WithMessage("Email length must be lower than 50 symbols.")
             .EmailAddress().WithMessage("Please enter a valid email format.");

            RuleFor(x => x.Password)
             .NotEmpty().WithMessage("Password cannot be empty.")
             .MinimumLength(8).WithMessage("Password must be at least 8 symbols long.")
             .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$")
             .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special symbol.");

        }
    }
}
