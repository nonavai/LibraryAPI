using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;

namespace BusinessLogic.Services;

public interface IAuthorService 
{
    Task<AuthorBookDto> GetByIdAsync(int id);
    Task<IEnumerable<AuthorDto>> GetAllAsync();
    Task<AuthorDto> AddAsync(AuthorClearDto model);
    Task<AuthorDto> UpdateAsync(int id, AuthorClearDto entity);
    Task<AuthorDto> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<BookFullDto>> GetBookByAuthor(int id);

}