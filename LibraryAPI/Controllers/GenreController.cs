﻿using AutoMapper;
using BusinessLogic.Models.Genre;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using LibraryAPI.Requests.Genre;
using LibraryAPI.Requests.User;
using LibraryAPI.Responses.Book;
using LibraryAPI.Responses.BookLoan;
using LibraryAPI.Responses.Genre;
using LibraryAPI.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly IMapper _mapper;
    

    public GenreController(IGenreService genreService, IMapper mapper)
    {
        _genreService = genreService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var dto = await _genreService.GetByIdAsync(id);
        var response = _mapper.Map<GenreBooksResponse>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAll()
    {
        var dto = await _genreService.GetAllAsync();
        var response = _mapper.Map<IEnumerable<GenreResponse>>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/Books")]
    public async Task<IActionResult> GetBooks([FromRoute] int id)
    {
        var dto = await _genreService.GetBooksByGenre(id);
        var response = _mapper.Map<IEnumerable<BookResponse>>(dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(GenreRequest entity)
    {
        var dto = _mapper.Map<GenreDto>(entity);
        var responseDto = await _genreService.AddAsync(dto);
        var response = _mapper.Map<GenreResponse>(responseDto);
        return Ok(response);
    }
    
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] GenreRequest entity)
    {
        var dto = _mapper.Map<GenreDto>(entity);
        dto.Id = id;
        var newUserDto = await _genreService.UpdateAsync(dto);
        var response = _mapper.Map<GenreResponse>(newUserDto);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var responseDto = await _genreService.DeleteAsync(id);
        var response = _mapper.Map<GenreResponse>(responseDto);
        return Ok(response);
    }
}