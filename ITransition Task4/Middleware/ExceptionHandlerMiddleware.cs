using Management.Service.Helpers;

namespace ITransition_Task4.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                context.Response.Redirect("/ErrorHandler/GlobalError?statusCode=404");
        }
        catch (ManagementException ex)
        {
            context.Response.Redirect($"/ErrorHandler/GlobalError?statusCode={ex.StatusCode}");
        }
        catch (Exception e)
        {
            context.Response.Redirect($"/ErrorHandler/GlobalError?statusCode=500");
        }
    }
}
