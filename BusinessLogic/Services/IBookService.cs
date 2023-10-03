using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BusinessLogic.Services;

public interface IBookService : IBaseService<BookDto>
{
    Task SetAvailable(int id, bool activity = true);
    Task<BookLoanDto> GetNewLoan(int bookId);
    Task<BookDto> AddRelations( RelationsDto dto);
}