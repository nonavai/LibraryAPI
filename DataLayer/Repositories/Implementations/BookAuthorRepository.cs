using DataLayer.DbContext;
using DataLayer.Entities;

namespace DataLayer.Repositories.Implementations;

public class BookAuthorRepository : IBookAuthorRepository
{
    private readonly LibraryContext db;

    public BookAuthorRepository(LibraryContext db)
    {
        this.db = db;
    }
    public async Task<BookAuthors> AddAsync(BookAuthors entity)
    {
        await db.BookAuthors.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}