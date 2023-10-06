using AutoMapper;
using BusinessLogic.Models.Author;
using BusinessLogic.Services;
using LibraryAPI.Requests.Author;
using LibraryAPI.Responses.Author;
using LibraryAPI.Responses.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IMapper _mapper;


    public AuthorController(IMapper mapper, IAuthorService authorService)
    {
        _mapper = mapper;
        _authorService = authorService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var dto = await _authorService.GetByIdAsync(id);
        var response = _mapper.Map<AuthorBooksResponse>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var dto = await _authorService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<AuthorResponse>>(dto);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}/Books")]
    public async Task<IActionResult> GetBooks([FromRoute] int id)
    {
        var dto = await _authorService.GetBookByAuthor(id);
        var response = _mapper.Map<IEnumerable<BookResponse>>(dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(AuthorRequest entity)
    {
        var dto = _mapper.Map<AuthorDto>(entity);
        var responseDto = await _authorService.AddAsync(dto);
        var response = _mapper.Map<AuthorResponse>(responseDto);
        return Ok(response);
    }

    
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] AuthorRequest entity)
    {
        var dto = _mapper.Map<AuthorDto>(entity);
        dto.Id = id;
        var newUserDto = await _authorService.UpdateAsync(dto);
        var response = _mapper.Map<AuthorResponse>(newUserDto);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var responseDto = await _authorService.DeleteAsync(id);
        var response = _mapper.Map<AuthorResponse>(responseDto);
        return Ok(response);
    }
}