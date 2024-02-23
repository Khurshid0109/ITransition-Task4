using Management.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using Management.Service.Interfaces;

namespace ITransition_Task4.Controllers;
public class AccessController : Controller
{
    private readonly IUserService _userService;

    public AccessController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ViewResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginPostModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.LoginAsync(model);

            if (result is true)
                return Redirect("~/Home/Index");
        }
        return View();
    }

    [HttpGet]
    public ViewResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserPostModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.AddAsync(model);
            return Redirect("~/Home/Index");
        }
        return View();
    }
}
