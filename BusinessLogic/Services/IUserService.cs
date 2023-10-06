using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.User;


namespace BusinessLogic.Services;

public interface IUserService : IBaseService<UserDto>
{
    Task<UserDto> GetByEmailAsync(string email);
    Task<bool> IsEmailExist(string email);
    Task<UserLoanDto> GetWithLoans(int id);
    Task<IQueryable<BookLoanDto>> GetLoansByUser(int id);
    Task<UserDto> LogInAsync(string email, string password);
}