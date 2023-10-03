namespace LibraryAPI.Requests.Book;

public record AddBookRelations
{
    public IEnumerable<int> Authors { get; set; }
    public IEnumerable<int> Genres { get; set; }
}