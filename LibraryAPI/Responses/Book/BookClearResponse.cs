namespace LibraryAPI.Responses.Book;

public class BookClearResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN13 { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
}