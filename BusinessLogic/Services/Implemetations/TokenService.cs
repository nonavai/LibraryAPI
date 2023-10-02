using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;
using DataAccess.Entities;
using DataLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Enums;
using Shared.Exceptions;


namespace BusinessLogic.Services.Implemetations;

public class TokenService : ITokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    

    public TokenService( IMapper mapper, IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository, IUserService userService)
    {
        _mapper = mapper;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _userService = userService;
    }
    public async Task<RefreshTokenDto> GenerateRefreshToken(UserDto user) //TODO: delete old token on creating
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);
        
        var refreshTokenDto = new RefreshTokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id,
            ExpiresAt = token.ValidTo
        };
        //delete old if already exist
        var oldToken = await GetByUserIdAsync(user.Id);
        if (oldToken != null && await _refreshTokenRepository.ExistsAsync(oldToken.Id))
        {
            await _refreshTokenRepository.DeleteAsync(oldToken.Id);
        }

        //creating
        var refreshToken = _mapper.Map<RefreshToken>(refreshTokenDto);
        var newRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
        refreshTokenDto.Id = newRefreshToken.Id;
        return refreshTokenDto;
    }

    public async Task<string> GenerateAccessToken(RefreshTokenDto refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var validationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidAudience = _configuration["JwtSettings:Audience"],
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ClockSkew = TimeSpan.Zero
        };
        SecurityToken validatedToken;
        try
        {
            tokenHandler.ValidateToken(refreshToken.Token, validationParameters, out validatedToken);
        }
        catch (SecurityTokenException)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Jti, refreshToken.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        };
        await AddClaimRoles(refreshToken.UserId, claims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshTokenDto?> GetByUserIdAsync(int id)
    {
        var refreshToken = await _refreshTokenRepository.GetByUserId(id);
        if (refreshToken == null)
        {
            return null;
        }
        var refreshTokenDto = _mapper.Map<RefreshTokenDto>(refreshToken);
        return refreshTokenDto;
    }
    
    public async Task<int> GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validToken = token.Split()[^1];
        var securityToken = tokenHandler.ReadJwtToken(validToken);
        if (securityToken == null)
        {
            throw new NotVerifiedException("Invalid token");
        }
        var claims = securityToken.Claims;
        var userIdClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti);
        if (userIdClaim == null)
        {
            throw new NotVerifiedException("Token does not contain a UserId claim");
        }

        var userId = Convert.ToInt32(userIdClaim.Value);
        return userId;
    }
    
    private async Task AddClaimRoles(int userId, List<Claim> claims)
    {
        var user = await _userService.GetByIdAsync(userId);
        switch (user.Role)
        {
            case Role.None:
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                break;
            case Role.Admin:
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                break;
            case Role.Author:
                claims.Add(new Claim(ClaimTypes.Role, "Author"));
                break;
        }
    }
}