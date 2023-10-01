using BusinessLogic.Models.BookLoan;

namespace BusinessLogic.Services;

public interface IBookLoanService 
{
     Task<BookLoanDto> GetByIdAsync(int id);
     Task<IEnumerable<BookLoanDto>> GetAllAsync();
     Task<BookLoanDto> AddAsync(BookLoanDto model);
     Task<BookLoanDto> CloseLoan(int id, DateTime returnDate);
     Task<bool> ExistsAsync(int id);
     Task<IQueryable<BookLoanDto>> GetByUserId(int id);
     Task<IQueryable<BookLoanDto>> GetByBookId(int id);
     
}