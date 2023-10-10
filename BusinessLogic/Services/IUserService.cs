using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.User;


namespace BusinessLogic.Services;

public interface IUserService 
{
    Task<UserDto> GetByIdAsync(int id);
    Task<IEnumerable<UserSecureDto>> GetAllAsync();
    Task<UserDto> AddAsync(UserClearDto model);
    Task<UserDto> UpdateAsync(int id, UserClearDto entity);
    Task<UserSecureDto> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<UserDto> GetByEmailAsync(string email);
    Task<bool> IsEmailExist(string email);
    Task<UserLoanDto> GetWithLoans(int id);
    Task<IQueryable<BookLoanDto>> GetLoansByUser(int id);
    Task<UserDto> LogInAsync(string email, string password);
}