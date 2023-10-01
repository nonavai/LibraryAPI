using Shared.Enums;

namespace DataLayer.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
    public Role Role { get; set; }
    public virtual IEnumerable<BookLoan> BookLoans {get; set; }
}