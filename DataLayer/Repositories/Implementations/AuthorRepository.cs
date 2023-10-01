using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
{
    private readonly LibraryContext db;
    public AuthorRepository(LibraryContext db) : base(db)
    {
        this.db = db;
    }
    
    public async Task<IQueryable<Book>> GetBookByAuthor(int id)
    {
        return db.Books.AsNoTracking().Where(b => b.Authors.Any(author => author.Id == id)).AsQueryable();
    }
}