using Business.DTOs.LibraryDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validations.FluentValidation.LibraryValidator
{
    public sealed class UpdateLibraryDtoValidator : AbstractValidator<UpdateLibraryDTO>
    {
        public UpdateLibraryDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("Game ID is required.");
        }
    }
}
