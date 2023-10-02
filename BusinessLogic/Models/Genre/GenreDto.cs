using BusinessLogic.Models.Book;

namespace BusinessLogic.Models.Genre;

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<BookClearDto> Books { get; set; }
}