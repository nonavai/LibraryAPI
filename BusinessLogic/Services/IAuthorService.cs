using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using DataLayer.Entities;

namespace BusinessLogic.Services;

public interface IAuthorService : IBaseService<AuthorDto>
{
    Task<IQueryable<BookDto>> GetBookByAuthor(int id);

}