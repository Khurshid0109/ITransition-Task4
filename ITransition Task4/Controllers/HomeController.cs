using System.Security.Claims;
using Management.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using Management.Service.Interfaces;

namespace ITransition_Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userList = await _userService.RetrieveAllAsync();

            return View(userList);
        }
       

        [HttpPost]
        public async Task<IActionResult> Block([FromBody] UserIdsModel model)
        {
            Console.WriteLine("hsevfsekfse");

            if (model != null && !string.IsNullOrEmpty(model.selectedUserIds))
            {
                var ids = model.selectedUserIds.Split(',').Select(id => long.Parse(id)).ToList();
                await _userService.BlockUsersAsync(ids);

                return Redirect("~/Access/Login");
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnBlock([FromBody] UserIdsModel model)
        {
            Console.WriteLine("hsevfsekfse");

            if (model != null && !string.IsNullOrEmpty(model.selectedUserIds))
            {
                var ids = model.selectedUserIds.Split(',').Select(id => long.Parse(id)).ToList();
                await _userService.UnBlockUsersAsync(ids);

                return Redirect("~/Home/Index");
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] UserIdsModel model)
        {
            Console.WriteLine("hsevfsekfse");

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            if (model != null && !string.IsNullOrEmpty(model.selectedUserIds))
            {
                var ids = model.selectedUserIds.Split(',').Select(id => long.Parse(id)).ToList();
                await _userService.RemoveUsersAsync(ids);

                return Redirect("~/Home/Index");
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

    }
}
