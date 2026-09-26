using Business.DTOs.CompanyDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validations.FluentValidation.CompanyValidator
{
    public sealed class CreateCompanyDtoValidator : AbstractValidator<CreateCompanyDTO>
    {
        public CreateCompanyDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name cannot be empty.")
                .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

            RuleFor(x => x.WebsiteUrl)
                .Must(LinkMustBeValid).When(x => !string.IsNullOrEmpty(x.WebsiteUrl))
                .WithMessage("Please enter a valid website URL.");
        }

        private bool LinkMustBeValid(string? link)
        {
            return Uri.TryCreate(link, UriKind.Absolute, out var outUri)
                   && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
