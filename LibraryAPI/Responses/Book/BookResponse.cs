using LibraryAPI.Responses.Author;
using LibraryAPI.Responses.Genre;

namespace LibraryAPI.Responses.Book;

public record BookResponse
{
    public string Title { get; set; }
    public string ISBN13 { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public IEnumerable<AuthorResponse> Authors { get; set; }
    public IEnumerable<GenreResponse> Genres { get; set; }
    
}