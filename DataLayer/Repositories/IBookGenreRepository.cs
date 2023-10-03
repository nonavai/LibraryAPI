using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IBookGenreRepository
{
    Task<BookGenres> AddAsync(BookGenres entity);
}