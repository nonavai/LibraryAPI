using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;

namespace BusinessLogic.Services;

public interface IBookService
{
    Task<BookDto> GetByIdAsync(int id);
    Task<IEnumerable<BookDto>> GetAllAsync();
    Task<BookClearDto> AddAsync(BookClearDto model);
    Task<BookClearDto> UpdateAsync(BookClearDto entity);
    Task<BookClearDto> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task SetAvailable(int id, bool activity = true);
    Task<BookLoanDto> GetNewLoan(int bookId);
    Task<BookDto> AddRelations( RelationsDto dto);
}