using BusinessLogic.Models.Book;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;


    public BookController( IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _bookService.GetByIdAsync(id);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _bookService.GetAllAsync();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/Loan")]
    public async Task<IActionResult> GetCurrentLoan([FromRoute] int id)
    {
        var response = await _bookService.GetNewLoan(id);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(BookClearDto request)
    {
        var response = await _bookService.AddAsync(request);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] BookClearDto request)
    {
        request.Id = id;
        var response = await _bookService.UpdateAsync(request);
        return Ok(response);
    }

    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var responseDto = await _bookService.DeleteAsync(id);
        return Ok(responseDto);
    }
    
    [Authorize]
    [HttpPost]
    [Route("{id:int}/AddRelations")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] RelationsDto request)
    {
        request.BookId = id;
        var response = await _bookService.AddRelations(request);
        return Ok(response);
    }
}