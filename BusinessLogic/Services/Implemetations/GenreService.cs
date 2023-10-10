using AutoMapper;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.Genre;
using DataLayer.Entities;
using DataLayer.Repositories;
using FluentValidation;
using Shared.Exceptions;
using ValidationException = Shared.Exceptions.ValidationException;


namespace BusinessLogic.Services.Implemetations;

public class GenreService: IGenreService
{
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;


    public GenreService( IMapper mapper, IGenreRepository genreRepository)
    {
        _mapper = mapper;
        _genreRepository = genreRepository;
    }


    public async Task<GenreBookDto> GetByIdAsync(int id)
    {
        var entity = await _genreRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<GenreBookDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        var dtos = _mapper.Map<IEnumerable<GenreDto>>( await _genreRepository.GetAllAsync());
        return dtos;
    }

    public async Task<GenreDto> AddAsync(GenreClearDto model)
    {
        var entity = _mapper.Map<Genre>(model);
        var dto = _mapper.Map<GenreDto>( await _genreRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<GenreDto> UpdateAsync(int id, GenreClearDto model)
    {
        var existingEntity = await _genreRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Genre not found");
        }
        //check on existing
        
        _mapper.Map(model, existingEntity);

        var dto = _mapper.Map<GenreDto>(await _genreRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<GenreDto> DeleteAsync(int id)
    {
        var entity = await _genreRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Genre not found");
        }
        
        var dto = _mapper.Map<GenreDto>( await _genreRepository.DeleteAsync(id));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _genreRepository.ExistsAsync(id);
    }

    public async Task<IEnumerable<BookFullDto>> GetBooksByGenre(int id)
    {
        var dtos = _mapper.Map<IEnumerable<BookFullDto>>( await _genreRepository.GetBooksByGenre(id));
        return dtos;
    }
}