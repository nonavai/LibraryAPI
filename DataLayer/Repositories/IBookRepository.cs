using DataLayer.Entities;

namespace DataLayer.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
     Task<BookLoan?> GetNewLoan(int bookId);
}