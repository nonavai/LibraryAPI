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


    public BookService(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository, IGenreRepository genreRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
        _genreRepository = genreRepository;
    }


    public async Task<BookFullDto> GetByIdAsync(int id)
    {
        var entity = await _bookRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<BookFullDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<BookFullDto>> GetAllAsync()
    {
        var entities = await _bookRepository.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<BookFullDto>>(entities);
        return dtos;
    }

    public async Task<BookDto> AddAsync(BookClearDto model)
    {
        var entity = _mapper.Map<Book>(model);
        entity.IsAvailable = true;
        var dto = _mapper.Map<BookDto>( await _bookRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<BookDto> UpdateAsync(int id, BookClearDto model)
    {
        var existingEntity = await _bookRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Book not found");
        }
        //check on existing

        _mapper.Map(model, existingEntity);
        
        var dto = _mapper.Map<BookDto>(await _bookRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<BookDto> DeleteAsync(int id)
    {
        var entity = await _bookRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Book not found");
        }
        
        var dto = _mapper.Map<BookDto>( await _bookRepository.DeleteAsync(id));
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

    public async Task<BookFullDto> AddRelations(RelationsDto dto)
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
        var bookDto = _mapper.Map<BookFullDto>(await _bookRepository.GetByIdAsync(dto.BookId));
        return bookDto;
    }
}