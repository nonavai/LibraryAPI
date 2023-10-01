using DataAccess.Entities;
using DataLayer.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class RefreshTokenRepository : GenericRepository<RefreshToken> , IRefreshTokenRepository
{
    private LibraryContext db;

    public RefreshTokenRepository(LibraryContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<RefreshToken?> GetByUserId(int id)
    {
        return await db.RefreshTokens.FirstOrDefaultAsync(f => f.UserId == id);
    }
    
}