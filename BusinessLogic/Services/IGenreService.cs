using BusinessLogic.Models.Book;
using BusinessLogic.Models.Genre;

namespace BusinessLogic.Services;

public interface IGenreService 
{
   Task<GenreDto> GetByIdAsync(int id);
   Task<IEnumerable<GenreClearDto>> GetAllAsync();
   Task<GenreClearDto> AddAsync(GenreClearDto model);
   Task<GenreClearDto> UpdateAsync(GenreClearDto entity);
   Task<GenreClearDto> DeleteAsync(int id);
   Task<bool> ExistsAsync(int id);
   Task<IEnumerable<BookDto>> GetBooksByGenre(int id);

}