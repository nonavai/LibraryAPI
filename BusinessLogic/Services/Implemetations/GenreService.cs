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
    private readonly IValidator<GenreClearDto> _validator;
    

    public GenreService( IMapper mapper, IValidator<GenreClearDto> validator, IGenreRepository genreRepository)
    {
        _mapper = mapper;
        _validator = validator;
        _genreRepository = genreRepository;
    }


    public async Task<GenreDto> GetByIdAsync(int id)
    {
        var entity = await _genreRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<GenreDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<GenreClearDto>> GetAllAsync()
    {
        var dtos = _mapper.Map<IEnumerable<GenreClearDto>>( await _genreRepository.GetAllAsync());
        return dtos;
    }

    public async Task<GenreClearDto> AddAsync(GenreClearDto model)
    {
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        var entity = _mapper.Map<Genre>(model);
        var dto = _mapper.Map<GenreClearDto>( await _genreRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<GenreClearDto> UpdateAsync(GenreClearDto model)
    {
        var existingEntity = await _genreRepository.GetByIdAsync(model.Id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Genre not found");
        }
        //check on existing
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        existingEntity.Name = model.Name;

        var dto = _mapper.Map<GenreClearDto>(await _genreRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<GenreClearDto> DeleteAsync(int id)
    {
        var entity = await _genreRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Genre not found");
        }
        
        var dto = _mapper.Map<GenreClearDto>( await _genreRepository.DeleteAsync(id));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _genreRepository.ExistsAsync(id);
    }

    public async Task<IEnumerable<BookDto>> GetBooksByGenre(int id)
    {
        var dtos = _mapper.Map<IEnumerable<BookDto>>( await _genreRepository.GetBooksByGenre(id));
        return dtos;
    }
}