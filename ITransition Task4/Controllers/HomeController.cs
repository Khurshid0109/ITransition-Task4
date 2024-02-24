using Management.Service.DTOs;
using Management.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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

            if (model != null && !string.IsNullOrEmpty(model.selectedUserIds))
            {
                try
                {
                    var ids = model.selectedUserIds.Split(',').Select(id => long.Parse(id)).ToList();

                    await _userService.BlockUsersAsync(ids);

                    // Get current user id
                    var userId = GetUserId();

                    if (ids.Contains(userId) || userId == 0)
                        return StatusCode(428, "You have just blocked yourself.");

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message); 
                }
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UnBlock([FromBody] UserIdsModel model)
        {

            if (model != null && !string.IsNullOrEmpty(model.selectedUserIds))
            {
                try
                {
                    var ids = model.selectedUserIds.Split(',').Select(id => long.Parse(id)).ToList();
                    await _userService.UnBlockUsersAsync(ids);

                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] UserIdsModel model)
        {

            if (model != null && !string.IsNullOrEmpty(model.selectedUserIds))
            {
                try
                {
                    var ids = model.selectedUserIds.Split(',').Select(id => long.Parse(id)).ToList();
                    
                    await _userService.RemoveUsersAsync(ids);

                    // Get current user id
                    var userId = GetUserId();

                    if (ids.Contains(userId) || userId == 0)
                        return StatusCode(403, "You have just deleted yourself.");

                    return Ok();
                }
                catch (Exception e) 
                { 
                    return BadRequest(e.Message); 
                }
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }

        private long GetUserId()
        {
            var token = HttpContext.Request.Cookies["token"];

            if (string.IsNullOrEmpty(token))
                return 0;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.First(claim => claim.Type == "Id").Value;
                return long.Parse(userId);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
