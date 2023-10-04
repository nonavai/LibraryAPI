using DataLayer.DbContext;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    private readonly LibraryContext db;

    public GenreRepository(LibraryContext db) : base(db)
    {
        this.db = db;
    }
    public override async Task<Genre?> GetByIdAsync(int id)
    {
        return await db.Genres
            //.AsNoTracking()
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public override async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return db.Genres
            .AsNoTracking()
            .Include(b => b.Books)
            .AsEnumerable();
    }


    public async Task<IQueryable<Book>> GetBooksByGenre(int id)
    {
        return db.Books/*.AsNoTracking()*/.Where(b => b.Genres.Any(genre => genre.Id == id)).AsQueryable();
    }
}