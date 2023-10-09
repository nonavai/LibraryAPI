using BusinessLogic.Models.Author;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;


    public AuthorController( IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var dto = await _authorService.GetByIdAsync(id);
        return Ok(dto);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var dto = await _authorService.GetAllAsync();
        return Ok(dto);
    }

    [HttpGet]
    [Route("{id:int}/Books")]
    public async Task<IActionResult> GetBooks([FromRoute] int id)
    {
        var dto = await _authorService.GetBookByAuthor(id);
        return Ok(dto);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(AuthorClearDto request)
    {
        var responseDto = await _authorService.AddAsync(request);
        return Ok(responseDto);
    }
    
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] AuthorClearDto request)
    {
        request.Id = id;
        var response = await _authorService.UpdateAsync(request);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _authorService.DeleteAsync(id);
        return Ok(response);
    }
}