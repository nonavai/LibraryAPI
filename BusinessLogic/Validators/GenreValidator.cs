using BusinessLogic.Models.Genre;
using FluentValidation;

namespace BusinessLogic.Validators;

public class GenreValidator : AbstractValidator<GenreClearDto>
{
    public GenreValidator()
    {
        RuleFor(genre => genre.Name)
            .NotEmpty().WithMessage("Genre name is required.")
            .MaximumLength(50).WithMessage("Genre name cannot exceed 50 characters.");
    }
}