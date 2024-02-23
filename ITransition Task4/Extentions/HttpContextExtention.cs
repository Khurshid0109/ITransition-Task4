using Management.Service.DTOs;

namespace ITransition_Task4.Extentions;
public static class HttpContextExtention
{
    public static UserViewModel? GetUser(this HttpContext httpContext)
    {
        var user = httpContext.Items["User"] as UserViewModel;
        return user;
    }
}
