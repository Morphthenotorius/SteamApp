using Business.DTOs.CategoryDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validations.FluentValidation.CategoryValidator
{
    public sealed class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDTO>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty.")
                .Length(2, 50).WithMessage("Category name must be between 2 and 50 characters.");
        }
    }
}
