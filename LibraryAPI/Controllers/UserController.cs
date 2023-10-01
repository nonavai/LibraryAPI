using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using LibraryAPI.Requests.User;
using LibraryAPI.Responses.BookLoan;
using LibraryAPI.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, IMapper mapper, ITokenService tokenService)
    {
        _userService = userService;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var dto = await _userService.GetByIdAsync(id);
        var response = _mapper.Map<UserResponse>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/all")]
    public async Task<IActionResult> GetUserLoan([FromRoute] int id)
    {
        var dto = await _userService.GetWithLoans(id);
        var response = _mapper.Map<UserLoanResponse>(dto);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/Loans")]
    public async Task<IActionResult> GetLoanByUser([FromRoute] int id)
    {
        var dto = await _userService.GetLoansByUser(id);
        var response = _mapper.Map<IQueryable<BookLoanResponse>>(dto);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> Login(LogInRequest entity)
    {
        var dto = await _userService.GetByEmailAsync(entity.Email);
        
        if (dto.Password != entity.Password)
        {
            return BadRequest("Wrong Password");
        }

        var response = _mapper.Map<LogInResponse>(dto);
        var newRefreshToken = await _tokenService.GenerateRefreshToken(dto);
        var accessToken = await _tokenService.GenerateAccessToken(newRefreshToken);
        /*response.RefreshToken = newRefreshToken.Token;
        response.accessToken = accessToken;*/
        Response.Cookies.Append("Authorization", accessToken);
        Response.Cookies.Append("AuthorizationRefresh", newRefreshToken.Token);
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(CreateUserRequest entity)
    {
        var dto = _mapper.Map<UserDto>(entity);
        var responseDto = await _userService.AddAsync(dto);
        var response = _mapper.Map<UserResponse>(responseDto);
        return Ok(response);
    }
    
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UserRequest entity)
    {
        
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var dto = _mapper.Map<UserDto>(entity);
        dto.Id = id;
        var newUserDto = await _userService.UpdateAsync(dto);
        var response = _mapper.Map<UserResponse>(newUserDto);
        return Ok(response);
    }
    
    //[ValidateToken] //to make it work - comment that attribute
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        var responseDto = await _userService.DeleteAsync(id);
        var response = _mapper.Map<UserDto>(responseDto);
        return Ok(response);
    }
    
    
    [HttpPost]
    [Route("UpdateToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["AuthorizationRefresh"];
        if (refreshToken == null)
        {
            return BadRequest("Invalid token");
        }

        var userId = await _tokenService.GetUserIdFromToken(refreshToken);
        var token = new RefreshTokenDto() 
            {
                UserId = userId,
                Token = refreshToken
            };
        var newAccessToken = await _tokenService.GenerateAccessToken(token);
        Response.Cookies.Delete("Authorization");
        Response.Cookies.Append("Authorization", newAccessToken);
        return Ok(newAccessToken);
    }
}