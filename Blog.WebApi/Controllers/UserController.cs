using Blog.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
  
    public class UserController : BaseController
    {
        #region Constructor
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region UserList
        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            return new JsonResult(await _userService.GetAllUsers());
        }
        #endregion
    }
}
