using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<IQueryable<Book>> GetBookByAuthor(int id);
}