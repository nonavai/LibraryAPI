using BusinessLogic.Models.Book;
using FluentValidation;

namespace BusinessLogic.Validators;

public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(book => book.ISBN13)
            .NotEmpty().WithMessage("ISBN-13 is required.")
            .Length(13).WithMessage("ISBN-13 must be exactly 13 characters.");

        RuleFor(book => book.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(book => book.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}


