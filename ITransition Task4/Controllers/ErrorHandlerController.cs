using Microsoft.AspNetCore.Mvc;

namespace ITransition_Task4.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [Route("/ErrorHandler/GlobalError")]
        public IActionResult GlobalError(int statusCode = 500)
        {
            ViewData["StatusCode"] = statusCode;

            return View();
        }
    }
}
