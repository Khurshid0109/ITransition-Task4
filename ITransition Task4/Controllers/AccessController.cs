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
            try
            {
                var result = await _userService.LoginAsync(model);

                // Check if the login was successful and an access token is available
                if (!string.IsNullOrEmpty(result.Token))
                {
                    // Set a cookie with the access token
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,  
                        SameSite = SameSiteMode.Strict,
                        Expires = result.AccessTokenExpireDate
                    };

                    Response.Cookies.Append("token", result.Token, cookieOptions);

                    // Redirect to the desired page after successful registration
                    return Redirect("~/Home/Index");
                }

                // Handle the case when registration was not successful
                ModelState.AddModelError("", "Login failed");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
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
            try
            {
                var result = await _userService.AddAsync(model);

                // Check if the registration was successful and an access token is available
                if (!string.IsNullOrEmpty(result.Token))
                {
                    // Set a cookie with the access token
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = result.AccessTokenExpireDate
                    };

                    Response.Cookies.Append("token", result.Token, cookieOptions);

                    // Redirect to the desired page after successful registration
                    return Redirect("~/Home/Index");
                }

                // Handle the case when registration was not successful
                ModelState.AddModelError("", "Registration failed");
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        return View();
    }
}
