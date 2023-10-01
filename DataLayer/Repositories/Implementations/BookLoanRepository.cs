using DataAccess.Entities;
using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class BookLoanRepository : GenericRepository<BookLoan>, IBookLoanRepository
{
    private readonly LibraryContext db;
    public BookLoanRepository(LibraryContext db) : base(db)
    {
        this.db = db;
    }

    public async Task<BookLoan?> GetByUserId(int id)
    {
        return await db.BookLoans.FirstOrDefaultAsync(b=> b.UserId == id);
    }

    public async Task<BookLoan?> GetByBookId(int id)
    {
        return await db.BookLoans.FirstOrDefaultAsync(b=> b.BookId == id);
    }
}