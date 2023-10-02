using DataLayer.Entities;

namespace DataAccess.Entities;

public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public virtual User User { get; set; }
}