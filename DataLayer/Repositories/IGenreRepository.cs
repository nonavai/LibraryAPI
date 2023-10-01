using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IGenreRepository : IBaseRepository<Genre>
{
    Task<IQueryable<Book>> GetBooksByGenre(int id);
}