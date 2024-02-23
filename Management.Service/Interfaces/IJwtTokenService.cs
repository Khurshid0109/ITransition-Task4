using Management.Service.DTOs;

namespace Management.Service.Interfaces;
public interface IJwtTokenService
{
    Task<(string token, DateTime tokenExpiryTime)> GenerateTokenAsync(UserViewModel user);
    Task<(string refreshToken, DateTime tokenValidityTime)> GenerateRefreshTokenAsync();
    Task<UserViewModel?> GetUserByAccessTokenAsync(string accessToken);
}
