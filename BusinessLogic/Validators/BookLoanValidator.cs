using BusinessLogic.Models.BookLoan;
using FluentValidation;

namespace BusinessLogic.Validators;

public class BookLoanValidator : AbstractValidator<BookLoanClearDto>
{
    public BookLoanValidator()
    {
        RuleFor(loan => loan.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");

        RuleFor(loan => loan.BookId)
            .GreaterThan(0).WithMessage("Book ID must be greater than 0.");

        RuleFor(loan => loan.LoanDate)
            .NotEmpty().WithMessage("Loan date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Loan date cannot be in the future.");

        RuleFor(loan => loan.ReturnDate)
            .Must(date => !date.HasValue || date != DateTime.MinValue && date != DateTime.MaxValue)
            .WithMessage("Invalid return date.")
            .When(loan => loan.ReturnDate.HasValue)
            .GreaterThanOrEqualTo(loan => loan.LoanDate)
            .When(loan => loan.ReturnDate.HasValue)
            .WithMessage("Return date must be greater than or equal to loan date if provided.");
    }
}