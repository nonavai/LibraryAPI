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
    
    public override async Task<Author?> GetByIdAsync(int id)
    {
        return await db.Authors
            //.AsNoTracking()
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public override async Task<IEnumerable<Author>> GetAllAsync()
    {
        return db.Authors
            .AsNoTracking()
            .Include(b => b.Books)
            .AsEnumerable();
    }
    
    public async Task<IEnumerable<Book>> GetBookByAuthor(int id)
    {
        return db.Books/*.AsNoTracking()*/.Where(b => b.Authors.Any(author => author.Id == id)).AsEnumerable();
    }
}