namespace LibraryAPI.Responses.User;

public record LogInResponse
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName  { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    /*public string accessToken { get; set; }
    public string RefreshToken { get; set; }*/
}