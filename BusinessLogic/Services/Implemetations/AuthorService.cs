using AutoMapper;
using BusinessLogic.Models.Author;
using BusinessLogic.Models.Book;
using DataLayer.Entities;
using DataLayer.Repositories;
using FluentValidation;
using Shared.Exceptions;
using ValidationException = Shared.Exceptions.ValidationException;


namespace BusinessLogic.Services.Implemetations;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;


    public AuthorService( IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }


    public async Task<AuthorBookDto> GetByIdAsync(int id)
    {
        var entity = await _authorRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<AuthorBookDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<AuthorDto>> GetAllAsync()
    {
        var dtos = _mapper.Map<IEnumerable<AuthorDto>>( await _authorRepository.GetAllAsync());
        return dtos;
    }

    public async Task<AuthorDto> AddAsync(AuthorClearDto model)
    {
        var entity = _mapper.Map<Author>(model);
        var dto = _mapper.Map<AuthorDto>( await _authorRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<AuthorDto> UpdateAsync(int id, AuthorClearDto model)
    {
        var existingEntity = await _authorRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Author not found");
        }
        //check on existing

        _mapper.Map(model, existingEntity);
        
        var dto = _mapper.Map<AuthorDto>(await _authorRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<AuthorDto> DeleteAsync(int id)
    {
        var entity = await _authorRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Author not found");
        }
        
        var dto = _mapper.Map<AuthorDto>( await _authorRepository.DeleteAsync(id));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _authorRepository.ExistsAsync(id);
    }

    public async Task<IEnumerable<BookFullDto>> GetBookByAuthor(int id)
    {
        var dtos = _mapper.Map<IEnumerable<BookFullDto>>( await _authorRepository.GetBookByAuthor(id));
        return dtos;
    }
}