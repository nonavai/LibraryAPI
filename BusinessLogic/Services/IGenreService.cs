using BusinessLogic.Models.Book;
using BusinessLogic.Models.Genre;

namespace BusinessLogic.Services;

public interface IGenreService 
{
   Task<GenreBookDto> GetByIdAsync(int id);
   Task<IEnumerable<GenreDto>> GetAllAsync();
   Task<GenreDto> AddAsync(GenreClearDto model);
   Task<GenreDto> UpdateAsync(int id, GenreClearDto entity);
   Task<GenreDto> DeleteAsync(int id);
   Task<bool> ExistsAsync(int id);
   Task<IEnumerable<BookFullDto>> GetBooksByGenre(int id);

}