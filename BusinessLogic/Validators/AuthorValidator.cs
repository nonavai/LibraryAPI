using BusinessLogic.Models.Author;
using FluentValidation;

namespace BusinessLogic.Validators;

public class AuthorValidator : AbstractValidator<AuthorClearDto>
{
    public AuthorValidator()
    {
        RuleFor(author => author.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(author => author.Birth)
            .Must(date => (date != DateTime.MinValue && date != DateTime.MaxValue))
            .WithMessage("Invalid birth date.");

        RuleFor(author => author.Death)
            .Must((author, date) =>
            {
                if (date == DateTime.MinValue || date == DateTime.MaxValue) return false; // Invalid date
                return date >= author.Birth; // Death date must be greater than or equal to birth date
            })
            .When(author => true) // Only apply this rule if Death has a value.
            .WithMessage("Invalid death date. Death date must be greater than or equal to birth date.");

        RuleFor(author => author.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}