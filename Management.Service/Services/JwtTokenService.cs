using System.Text;
using System.Security.Claims;
using Management.Service.DTOs;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Management.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Management.Service.Services;
public class JwtTokenService:IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtTokenService> _logger;
    public JwtTokenService(IConfiguration configuration,
                           ILogger<JwtTokenService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public Task<(string refreshToken, DateTime tokenValidityTime)> GenerateRefreshTokenAsync()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        if (!double.TryParse(_configuration["JWT:RefreshTokenValidityHours"], out double refreshTokenValidityHours))
            refreshTokenValidityHours = 5;

        var tokenExpiryTime = DateTime.UtcNow.AddHours(refreshTokenValidityHours);
        return Task.FromResult((Convert.ToBase64String(randomNumber), tokenExpiryTime));
    }

    public Task<(string token, DateTime tokenExpiryTime)> GenerateTokenAsync(UserViewModel user)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);
        var expireDate = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:AccessTokenExpireMinutes"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim("Role",user.Role.ToString()),
                 new Claim("Email", user.Email),
                 new Claim("Name", user.FirstName + " " + user.LastName),
            }),
            Audience = _configuration["JWT:Audience"],
            Issuer = _configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = expireDate,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult((tokenHandler.WriteToken(token), expireDate));
    }
}
