using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IBookAuthorRepository
{
    Task<BookAuthors> AddAsync(BookAuthors entity);
}