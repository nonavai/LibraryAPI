using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetWithLoans(int id);
    Task<IQueryable<BookLoan>> GetLoansByUser(int id);
}