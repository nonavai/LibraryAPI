using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<IEnumerable<Book>> GetBookByAuthor(int id);
}