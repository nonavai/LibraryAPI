using AutoMapper;
using BusinessLogic.Models.BookLoan;
using BusinessLogic.Models.Genre;
using BusinessLogic.Services;
using LibraryAPI.Requests.BookLoan;
using LibraryAPI.Requests.Genre;
using LibraryAPI.Responses.Book;
using LibraryAPI.Responses.BookLoan;
using LibraryAPI.Responses.Genre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BookLoanController : ControllerBase
{
    private readonly IBookLoanService _bookLoanService;
    private readonly IMapper _mapper;


    public BookLoanController(IMapper mapper, IBookLoanService bookLoanService)
    {
        _mapper = mapper;
        _bookLoanService = bookLoanService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var dto = await _bookLoanService.GetByIdAsync(id);
        var response = _mapper.Map<BookLoanResponse>(dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddLoan(BookLoanRequest entity)
    {
        var dto = _mapper.Map<BookLoanDto>(entity);
        var responseDto = await _bookLoanService.AddAsync(dto);
        var response = _mapper.Map<BookLoanResponse>(responseDto);
        return Ok(response);
    }

    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> CloseLoan([FromRoute] int id, [FromBody] DateTime returnDate)
    {
        var newUserDto = await _bookLoanService.CloseLoan(id, returnDate);
        var response = _mapper.Map<BookLoanResponse>(newUserDto);
        return Ok(response);
    }
    
}