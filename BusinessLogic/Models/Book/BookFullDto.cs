using BusinessLogic.Models.Author;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;

namespace BusinessLogic.Models.Book;

public class BookFullDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN13 { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsAvailable { get; set; }
    public IEnumerable<AuthorDto> Authors { get; set; }
    public IEnumerable<GenreDto> Genres { get; set; }
    
}