using BusinessLogic.Models.BookLoan;
using Shared.Enums;

namespace BusinessLogic.Models.User;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
    public Role Role { get; set; }
    
}