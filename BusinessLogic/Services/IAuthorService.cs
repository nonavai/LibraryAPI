using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;

namespace BusinessLogic.Services;

public interface IAuthorService : IBaseService<AuthorDto>
{
    Task<IEnumerable<BookDto>> GetBookByAuthor(int id);

}