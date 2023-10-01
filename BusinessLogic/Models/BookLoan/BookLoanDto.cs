using BusinessLogic.Models.Book;
using BusinessLogic.Models.User;

namespace BusinessLogic.Models.BookLoan;

public class BookLoanDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    
   
    
}