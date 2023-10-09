using BusinessLogic.Models.Genre;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;


    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _genreService.GetByIdAsync(id);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _genreService.GetAllAsync();
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/Books")]
    public async Task<IActionResult> GetBooks([FromRoute] int id)
    {
        var response = await _genreService.GetBooksByGenre(id);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(GenreClearDto request)
    {
        var response = await _genreService.AddAsync(request);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] GenreClearDto request)
    {
        request.Id = id;
        var response = await _genreService.UpdateAsync(request);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var response = await _genreService.DeleteAsync(id);
        return Ok(response);
    }
}