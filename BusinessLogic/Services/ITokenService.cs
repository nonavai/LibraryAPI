using BusinessLogic.Models.RefreshToken;
using BusinessLogic.Models.User;

namespace BusinessLogic.Services;

public interface ITokenService
{
    Task<RefreshTokenDto> GenerateRefreshToken(UserDto userId);
    Task<string> GenerateAccessToken(RefreshTokenDto refreshTokenDto);
    Task<RefreshTokenDto?> GetByUserIdAsync(int id);
    Task<int> GetUserIdFromToken(string token);
    Task<string> RefreshToken(string? refreshToken);

}