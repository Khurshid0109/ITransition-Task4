using ITransition_Task4.Extentions;
using ITransition_Task4.Middleware;
using Management.Data.DbContexts;
using Management.Service.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddService();
builder.Services.AddAutoMapper(typeof(MapperProfile));

//for Jwt authentication
builder.Services.AddJwtService(builder.Configuration);

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    await next();


    if (ctx.Response.StatusCode == 404)
    {
        ctx.Request.Path = "/ErrorHandler/GlobalError?statusCode=" + ctx.Response.StatusCode;
        await next();
    }

});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(cors =>
    cors.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Register}");

app.MapControllerRoute(
    name: "Main",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
