using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;

namespace BusinessLogic.Services;

public interface IAuthorService 
{
    Task<AuthorDto> GetByIdAsync(int id);
    Task<IEnumerable<AuthorClearDto>> GetAllAsync();
    Task<AuthorClearDto> AddAsync(AuthorClearDto model);
    Task<AuthorClearDto> UpdateAsync(AuthorClearDto entity);
    Task<AuthorClearDto> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<BookDto>> GetBookByAuthor(int id);

}