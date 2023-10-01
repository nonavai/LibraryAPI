using LibraryAPI.Responses.BookLoan;
using Shared.Enums;

namespace LibraryAPI.Responses.User;

public record UserLoanResponse
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
    public virtual IEnumerable<BookLoanResponse> BookLoans {get; set; }
}