using BusinessLogic.Models.Book;
using FluentValidation;

namespace BusinessLogic.Validators;

public class BookValidator : AbstractValidator<BookClearDto>
{
    public BookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(book => book.ISBN13)
            .NotEmpty().WithMessage("ISBN-13 is required.")
            .Must(IsValidIsbn);

        RuleFor(book => book.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(book => book.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
    
    private bool IsValidIsbn(string isbn)
    {
        
        isbn = isbn.Replace("-", "").Replace(" ", "");
        if (isbn.Length == 13)
        {
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
            }
            int expectedCheckDigit = (10 - (sum % 10)) % 10;
            return isbn[12] == expectedCheckDigit.ToString()[0];
        }

        return false;
    }
}


