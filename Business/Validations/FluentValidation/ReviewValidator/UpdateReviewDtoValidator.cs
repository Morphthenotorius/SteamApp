using Business.DTOs.ReviewDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validations.FluentValidation.ReviewValidator
{
    public sealed class UpdateReviewDtoValidator : AbstractValidator<UpdateReviewDTO>
    {
        public UpdateReviewDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Review content cannot be empty.")
                .MinimumLength(10).WithMessage("Review must be at least 10 characters long.")
                .MaximumLength(1000).WithMessage("Review cannot exceed 1000 characters.");
        }
    }
}
