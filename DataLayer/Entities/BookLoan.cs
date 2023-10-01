namespace DataLayer.Entities;

public class BookLoan : BaseEntity
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public virtual User User { get; set; }
    public virtual Book Book { get; set; }
}