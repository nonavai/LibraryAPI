using AutoMapper;
using BusinessLogic.Models.BookLoan;
using DataLayer.Entities;
using DataLayer.Repositories;
using FluentValidation;
using Shared.Exceptions;
using ValidationException = Shared.Exceptions.ValidationException;

namespace BusinessLogic.Services.Implemetations;

public class BookLoanService : IBookLoanService
{
    private readonly IBookLoanRepository _bookLoanRepository;
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;
    private readonly IValidator<BookLoanDto> _validator;
    

    public BookLoanService( IMapper mapper, IValidator<BookLoanDto> validator, IBookLoanRepository bookLoanRepository, IBookService bookService)
    {
        _mapper = mapper;
        _validator = validator;
        _bookLoanRepository = bookLoanRepository;
        _bookService = bookService;
    }


    public async Task<BookLoanDto> GetByIdAsync(int id)
    {
        var entity = await _bookLoanRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new NotFoundException("Not found");
        }
        var dto = _mapper.Map<BookLoanDto>(entity);
        return dto;
    }

    public async Task<IEnumerable<BookLoanDto>> GetAllAsync()
    {
        var dtos = _mapper.Map<IEnumerable<BookLoanDto>>( await _bookLoanRepository.GetAllAsync());
        return dtos;
    }

    public async Task<BookLoanDto> AddAsync(BookLoanDto model)
    {
        var validationResult = _validator.Validate(model);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.ToString());
        }
        var entity = _mapper.Map<BookLoan>(model);
        await _bookService.SetAvailable(model.BookId, false);
        var dto = _mapper.Map<BookLoanDto>( await _bookLoanRepository.AddAsync(entity));
        return dto;
    }
    

    public async Task<BookLoanDto> CloseLoan(int id, DateTime returnDate)
    {
        var existingEntity = await _bookLoanRepository.GetByIdAsync(id);
        if (existingEntity == null)
        {
            throw new NotFoundException("Book not found");
        }

        await _bookService.SetAvailable(existingEntity.BookId);
        existingEntity.ReturnDate = returnDate;
        
        var dto = _mapper.Map<BookLoanDto>(await _bookLoanRepository.UpdateAsync(existingEntity));
        return dto;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _bookLoanRepository.ExistsAsync(id);
    }

    public async Task<IQueryable<BookLoanDto>> GetByUserId(int id)
    {
        var entitys = await _bookLoanRepository.GetByUserId(id);
        if (entitys == null)
        {
            throw new NotFoundException("BookLoans not found");
        }
        var dtos = _mapper.Map<IQueryable<BookLoanDto>>(entitys);
        return dtos;
    }

    public async Task<IQueryable<BookLoanDto>> GetByBookId(int id)
    {
        var entitys = await _bookLoanRepository.GetByBookId(id);
        if (entitys == null)
        {
            throw new NotFoundException("BookLoans not found");
        }
        var dtos = _mapper.Map<IQueryable<BookLoanDto>>(entitys);
        return dtos;
    }
}