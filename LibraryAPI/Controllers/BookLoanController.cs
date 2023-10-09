using BusinessLogic.Models.BookLoan;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BookLoanController : ControllerBase
{
    private readonly IBookLoanService _bookLoanService;

    public BookLoanController(IBookLoanService bookLoanService)
    {
        _bookLoanService = bookLoanService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _bookLoanService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddLoan(BookLoanClearDto request)
    {
        var response = await _bookLoanService.AddAsync(request);
        return Ok(response);
    }

    
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> CloseLoan([FromRoute] int id, [FromBody] DateTime returnDate)
    {
        var response = await _bookLoanService.CloseLoan(id, returnDate);
        return Ok(response);
    }
    
}