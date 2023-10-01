namespace LibraryAPI.Requests.Book;

public record BookRequest
{
    public string Title { get; set; }
    public string ISBN13 { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}