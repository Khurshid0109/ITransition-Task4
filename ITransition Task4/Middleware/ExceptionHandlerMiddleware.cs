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
        }
        catch (ManagementException ex)
        {
            Console.WriteLine(ex.Message);
            context.Response.Redirect($"/ErrorHandler/GlobalError?statusCode={ex.StatusCode}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            context.Response.Redirect($"/ErrorHandler/GlobalError?statusCode=500");
        }
    }
}
