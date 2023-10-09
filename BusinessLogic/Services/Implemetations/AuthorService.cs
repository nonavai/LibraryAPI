﻿using AutoMapper;
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
    private readonly IValidator<AuthorClearDto> _validator;
    

    public AuthorService( IMapper mapper, IAuthorRepository authorRepository, IValidator<AuthorClearDto> validator)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
        _validator = validator;
    }


    public async Task<AuthorDto> GetByIdAsync(int id)
    {
        var entity = await _authorRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<AuthorDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<AuthorClearDto>> GetAllAsync()
    {
        var dtos = _mapper.Map<IEnumerable<AuthorClearDto>>( await _authorRepository.GetAllAsync());
        return dtos;
    }

    public async Task<AuthorClearDto> AddAsync(AuthorClearDto model)
    {
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        var entity = _mapper.Map<Author>(model);
        var dto = _mapper.Map<AuthorClearDto>( await _authorRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<AuthorClearDto> UpdateAsync(AuthorClearDto model)
    {
        var existingEntity = await _authorRepository.GetByIdAsync(model.Id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Author not found");
        }
        //check on existing
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }

        existingEntity.Name = model.Name;
        existingEntity.Description = model.Description;


        var dto = _mapper.Map<AuthorClearDto>(await _authorRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<AuthorClearDto> DeleteAsync(int id)
    {
        var entity = await _authorRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Author not found");
        }
        
        var dto = _mapper.Map<AuthorClearDto>( await _authorRepository.DeleteAsync(id));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _authorRepository.ExistsAsync(id);
    }

    public async Task<IEnumerable<BookDto>> GetBookByAuthor(int id)
    {
        var dtos = _mapper.Map<IEnumerable<BookDto>>( await _authorRepository.GetBookByAuthor(id));
        return dtos;
    }
}