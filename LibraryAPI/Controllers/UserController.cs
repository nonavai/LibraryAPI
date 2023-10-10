using AutoMapper;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
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

    [Authorize/*(Roles = Roles.Admin)*/]
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var response = await _userService.GetByIdAsync(id);
        return Ok(response);
    }
    
    [Authorize/*(Roles = Roles.Admin)*/]
    [HttpGet]
    [Route("{id:int}/full")]
    public async Task<IActionResult> GetUserLoan([FromRoute] int id)
    {
        var response = await _userService.GetWithLoans(id);
        return Ok(response);
    }
    
    [HttpGet]
    [Route("{id:int}/Loans")]
    public async Task<IActionResult> GetLoanByUser([FromRoute] int id)
    {
        var response = await _userService.GetLoansByUser(id);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("LogIn")]
    public async Task<IActionResult> Login(LogInDto request)
    {
        var response = await _userService.LogInAsync(request.Email, request.Password);
        var newRefreshToken = await _tokenService.GenerateRefreshToken(response);
        var accessToken = await _tokenService.GenerateAccessToken(newRefreshToken);
        Response.Cookies.Append("Authorization", accessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
        Response.Cookies.Append("AuthorizationRefresh", newRefreshToken.Token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
        return Ok(response);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Create(UserClearDto request)
    {
        var response = await _userService.AddAsync(request);
        return Ok(response);
    }
    
    //[ValidateToken] //check if user wants to edit himself
    [Authorize]
    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UserClearDto request)
    {
        var response = await _userService.UpdateAsync(id, request);
        return Ok(response);
    }
    
    //[ValidateToken] //check if user wants to delete himself
    [Authorize]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        var response = await _userService.DeleteAsync(id);
        return Ok(response);
    }
    
    
    [HttpPost]
    [Route("UpdateToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var newAccessToken = await _tokenService.RefreshToken(Request.Cookies["AuthorizationRefresh"]);
        Response.Cookies.Delete("Authorization", new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
        Response.Cookies.Append("Authorization", newAccessToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
        return Ok(newAccessToken);
    }
}