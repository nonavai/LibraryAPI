using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly LibraryContext db;
    
    public BookRepository(LibraryContext db) : base(db)
    {
        this.db = db;
    }

    public override async Task<Book?> GetByIdAsync(int id)
    {
        return await db.Books
            //.AsNoTracking()
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public override async Task<IEnumerable<Book>> GetAllAsync()
    {
        return db.Books
            .AsNoTracking()
            .Include(b => b.Authors)
            .Include(b => b.Genres)
            .AsEnumerable();
    }

    public async Task<BookLoan?> GetNewLoan(int bookId)
    {
        return await db.BookLoans
            .AsNoTracking().Where(bl=> bl.ReturnDate == null)
            .FirstOrDefaultAsync(bl => bl.BookId == bookId);

    }
}