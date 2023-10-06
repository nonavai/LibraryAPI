using LibraryAPI.Responses.Book;

namespace LibraryAPI.Responses.Author;

public record AuthorBooksResponse
{
    public string Name { get; set; }
    public DateTime? Birth { get; set; }
    public DateTime? Death { get; set; }
    public string? Description { get; set; }
    public IEnumerable<BookClearResponse> Books { get; set; }
}