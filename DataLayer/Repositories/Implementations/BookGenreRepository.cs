using DataLayer.DbContext;
using DataLayer.Entities;

namespace DataLayer.Repositories.Implementations;

public class BookGenreRepository : IBookGenreRepository
{
    private readonly LibraryContext db;

    public BookGenreRepository(LibraryContext db)
    {
        this.db = db;
    }
    public async Task<BookGenres> AddAsync(BookGenres entity)
    {
        await db.BookGenres.AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}