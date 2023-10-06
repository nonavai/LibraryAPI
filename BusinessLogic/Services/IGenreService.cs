using BusinessLogic.Models.Book;
using BusinessLogic.Models.Genre;

namespace BusinessLogic.Services;

public interface IGenreService : IBaseService<GenreDto>
{
   Task<IEnumerable<BookDto>> GetBooksByGenre(int id);

}