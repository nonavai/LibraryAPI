using DataAccess.Entities;

namespace DataLayer.Repositories;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
    Task<RefreshToken?> GetByUserId(int id);
}