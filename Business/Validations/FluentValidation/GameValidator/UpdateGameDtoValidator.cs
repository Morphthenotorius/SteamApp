using Business.DTOs.GameDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validations.FluentValidation.GameValidator
{
    public sealed class UpdateGameDtoValidator : AbstractValidator<UpdateGameDTO>
    {
        public UpdateGameDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Game title cannot be empty.")
                .MaximumLength(150).WithMessage("Game title cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty.")
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

            RuleFor(x => x.CoverImageUrl)
                .NotEmpty().WithMessage("Cover image URL cannot be empty.");

            RuleFor(x => x.ReleaseDate)
                .NotEmpty().WithMessage("Release date is required.");

            RuleFor(x => x.PublisherId)
                .NotEmpty().WithMessage("Publisher is required.");

            RuleFor(x => x.DeveloperId)
                .NotEmpty().WithMessage("Developer is required.");

            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("At least one category must be selected.");
        }
    }
}
