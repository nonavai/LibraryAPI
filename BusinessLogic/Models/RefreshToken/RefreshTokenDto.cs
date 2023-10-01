namespace BusinessLogic.Models.RefreshToken;

public class RefreshTokenDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}