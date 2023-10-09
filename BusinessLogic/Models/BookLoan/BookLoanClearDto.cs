namespace BusinessLogic.Models.BookLoan;

public class BookLoanClearDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}