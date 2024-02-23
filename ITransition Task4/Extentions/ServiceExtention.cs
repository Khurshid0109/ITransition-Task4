using System.Text;
using Management.Service.Services;
using Management.Data.Repositories;
using Management.Data.IRepositories;
using Management.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ITransition_Task4.Extentions;
public static class ServiceExtention
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IRepository,Repository>();
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<IJwtTokenService,JwtTokenService>();
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
