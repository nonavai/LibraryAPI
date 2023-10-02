using LibraryAPI.Responses.Book;

namespace LibraryAPI.Responses.Genre;

public record GenreBooksResponse
{
    public string Name { get; set; }
    public virtual IEnumerable<BookClearResponse> Books { get; set; }
}