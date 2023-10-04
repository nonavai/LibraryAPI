namespace BusinessLogic.Models.Book;

public class RelationsDto
{
    public int BookId { get; set; }
    public IEnumerable<int> Authors { get; set; }
    public IEnumerable<int> Genres { get; set; }
}