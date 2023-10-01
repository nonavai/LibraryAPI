namespace LibraryAPI.Requests.BookLoan;

public record BookLoanRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}