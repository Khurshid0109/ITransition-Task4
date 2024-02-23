using Management.Service.Services;
using Management.Data.Repositories;
using Management.Data.IRepositories;
using Management.Service.Interfaces;

namespace ITransition_Task4.Extentions;
public static class ServiceExtention
{
    public static void AddService(this IServiceCollection services)
    {
        services.AddScoped<IRepository,Repository>();
        services.AddScoped<IUserService,UserService>();
    }
}
