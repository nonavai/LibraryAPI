namespace DataLayer.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public string ISBN13 { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public virtual IEnumerable<Author> Authors { get; set; }
    public virtual IEnumerable<Genre> Genres { get; set; }
    public virtual IEnumerable<BookLoan> BookLoans { get; set; }
}