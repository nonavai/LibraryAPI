namespace DataLayer.Entities;

public class Author : BaseEntity
{
    
    public string Name { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime? Death { get; set; }
    public string? Description { get; set; }
    public virtual IEnumerable<Book> Books { get; set; }
}