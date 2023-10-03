using AutoMapper;
using BusinessLogic.Models.Book;
using BusinessLogic.Services;
using LibraryAPI.Requests.Book;
using LibraryAPI.Responses.Book;
using LibraryAPI.Responses.BookLoan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;


    public BookController(IMapper mapper, IBookService bookService)
    {
        _mapper = mapper;
        _bookService = bookService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var dto = await _bookService.GetByIdAsync(id);
        var response = _mapper.Map<BookResponse>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var dto = await _bookService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<BookResponse>>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/Loan")]
    public async Task<IActionResult> GetCurrentLoan([FromRoute] int id)
    {
        var dto = await _bookService.GetNewLoan(id);
        var response = _mapper.Map<BookLoanResponse>(dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(BookRequest entity)
    {
        var dto = _mapper.Map<BookDto>(entity);
        var responseDto = await _bookService.AddAsync(dto);
        var response = _mapper.Map<BookResponse>(responseDto);
        return Ok(response);
    }

    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] BookRequest entity)
    {

        if (!await _bookService.ExistsAsync(id))
        {
            return NotFound();
        }

        var dto = _mapper.Map<BookDto>(entity);
        dto.Id = id;
        var newUserDto = await _bookService.UpdateAsync(dto);
        var response = _mapper.Map<BookResponse>(newUserDto);
        return Ok(response);
    }

    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!await _bookService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _bookService.DeleteAsync(id);
        var response = _mapper.Map<BookResponse>(responseDto);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPost]
    [Route("{id:int}/AddRelations")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] AddBookRelations request)
    {
        var dto = _mapper.Map<RelationsDto>(request);
        var responseDto = await _bookService.AddRelations(dto);
        var response = _mapper.Map<BookResponse>(responseDto);
        return Ok(response);
    }
}