namespace LibraryAPI.Requests.User;

public record UserRequest
{
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? Description { get; set; }
}