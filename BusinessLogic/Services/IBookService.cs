using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;

namespace BusinessLogic.Services;

public interface IBookService
{
    Task<BookFullDto> GetByIdAsync(int id);
    Task<IEnumerable<BookFullDto>> GetAllAsync();
    Task<BookDto> AddAsync(BookClearDto model);
    Task<BookDto> UpdateAsync(int id, BookClearDto entity);
    Task<BookDto> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task SetAvailable(int id, bool activity = true);
    Task<BookLoanDto> GetNewLoan(int bookId);
    Task<BookFullDto> AddRelations( RelationsDto dto);
}