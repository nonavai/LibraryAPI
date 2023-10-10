
using System.Text.RegularExpressions;
using BusinessLogic.Models.User;
using FluentValidation;

namespace BusinessLogic.Validators;

public class UserValidator : AbstractValidator<UserClearDto>
{
    public UserValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(user => user.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("Please provide a valid email address.");
        RuleFor(user => user.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(user => user.PhoneNumber).NotEmpty()
            .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex(@"^[+]?[(]?[0-9]{3}[)]?[-\s.]?[0-9]{3}[-\s.]?[0-9]{4,6}$")).WithMessage("PhoneNumber not valid");
    }
}