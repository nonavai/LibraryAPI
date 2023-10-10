using BusinessLogic.Models.Book;

namespace BusinessLogic.Models.Genre;

public class GenreBookDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<BookDto> Books { get; set; }
}