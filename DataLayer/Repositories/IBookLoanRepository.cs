using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IBookLoanRepository : IBaseRepository<BookLoan>
{
    Task<BookLoan?> GetByUserId(int id);
    Task<BookLoan?> GetByBookId(int id);
}