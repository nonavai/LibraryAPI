using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class UserRepository : GenericRepository<User> , IUserRepository
{
    private LibraryContext db;

    public UserRepository(LibraryContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetWithLoans(int id)
    {
        return await db.Users
            .AsNoTracking()
            .Include(u => u.BookLoans)
            .ThenInclude(b => b.Book)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IQueryable<BookLoan>> GetLoansByUser(int id)
    {
        return db.BookLoans
            .AsNoTracking()
            .Include(b => b.Book)
            .Where(b => b.User.Id == id).AsQueryable();
    }
}