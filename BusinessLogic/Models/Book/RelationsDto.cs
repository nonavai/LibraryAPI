namespace BusinessLogic.Models.Book;

public class RelationsDto
{
    public int Id { get; set; }
    public IEnumerable<int> Authors { get; set; }
    public IEnumerable<int> Genres { get; set; }
}