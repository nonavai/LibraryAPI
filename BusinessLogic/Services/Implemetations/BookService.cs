using AutoMapper;
using BusinessLogic.Models.Book;
using BusinessLogic.Models.BookLoan;
using DataLayer.Entities;
using DataLayer.Repositories;
using FluentValidation;
using Shared.Exceptions;
using ValidationException = Shared.Exceptions.ValidationException;

namespace BusinessLogic.Services.Implemetations;

public class BookService :  IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<BookClearDto> _validator;
    

    public BookService(IBookRepository bookRepository, IMapper mapper, IValidator<BookClearDto> validator, IAuthorRepository authorRepository, IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _validator = validator;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
    }


    public async Task<BookDto> GetByIdAsync(int id)
    {
        var entity = await _bookRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<BookDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var entities = await _bookRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<BookDto>>(entities);
        return dtos;
    }

    public async Task<BookClearDto> AddAsync(BookClearDto model)
    {
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        var entity = _mapper.Map<Book>(model);
        entity.IsAvailable = true;
        var dto = _mapper.Map<BookClearDto>( await _bookRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<BookClearDto> UpdateAsync(BookClearDto model)
    {
        var existingEntity = await _bookRepository.GetByIdAsync(model.Id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Book not found");
        }
        //check on existing
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }

        existingEntity.Name = model.Name;
        existingEntity.Title = model.Title;
        existingEntity.Description = model.Description;


        var dto = _mapper.Map<BookClearDto>(await _bookRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<BookClearDto> DeleteAsync(int id)
    {
        var entity = await _bookRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Book not found");
        }
        
        var dto = _mapper.Map<BookClearDto>( await _bookRepository.DeleteAsync(id));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _bookRepository.ExistsAsync(id);
    }

    public async Task SetAvailable(int id, bool activity = true)
    {
        var existingEntity = await _bookRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Book not found");
        }
        existingEntity.IsAvailable = activity;
    }

    public async Task<BookLoanDto> GetNewLoan(int bookId)
    {
        var existingEntity = await _bookRepository.GetNewLoan(bookId);
        if (existingEntity == null)
        {
            throw new NotFoundException("BookLoan not found");
        }
        var dto = _mapper.Map<BookLoanDto>(existingEntity);
        return dto;
    }

    public async Task<BookDto> AddRelations(RelationsDto dto)
    {
        var book = await _bookRepository.GetByIdAsync(dto.BookId);
        if (book == null) throw new NotFoundException();
        foreach (var id in dto.Genres)
        {
            book.Genres.Add(await _genreRepository.GetByIdAsync(id));
        }
        
        foreach (var id in dto.Authors)
        {
            book.Authors.Add(await _authorRepository.GetByIdAsync(id));
        }

        await _bookRepository.UpdateAsync(book);
        var bookDto = _mapper.Map<BookDto>(await _bookRepository.GetByIdAsync(dto.BookId));
        return bookDto;
    }
}