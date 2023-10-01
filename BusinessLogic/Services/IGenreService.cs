using BusinessLogic.Models.Book;
using BusinessLogic.Models.Genre;

namespace BusinessLogic.Services;

public interface IGenreService : IBaseService<GenreDto>
{
   Task<IQueryable<BookDto>> GetBooksByGenre(int id);

}