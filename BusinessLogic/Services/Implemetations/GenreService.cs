using AutoMapper;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.Genre;
using BusinessLogic.Validators;
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
    private readonly IValidator<GenreDto> _validator;
    

    public GenreService( IMapper mapper, IValidator<GenreDto> validator, IGenreRepository genreRepository)
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

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        var dtos = _mapper.Map<IEnumerable<GenreDto>>( await _genreRepository.GetAllAsync());
        return dtos;
    }

    public async Task<GenreDto> AddAsync(GenreDto model)
    {
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        var entity = _mapper.Map<Genre>(model);
        var dto = _mapper.Map<GenreDto>( await _genreRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<GenreDto> UpdateAsync(GenreDto model)
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

    public async Task<IQueryable<BookDto>> GetBooksByGenre(int id)
    {
        var dtos = _mapper.Map<IQueryable<BookDto>>( await _genreRepository.GetBooksByGenre(id));
        return dtos;
    }
}