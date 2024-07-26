using Blog.Core.DTOs.Account;
using Blog.Core.Services.Implementation;
using Blog.Core.Services.Interfaces;
using Blog.Core.Utils.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.WebApi.Controllers
{

    public class AccountController : BaseController
    {
        #region costructor

        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


        #region Register

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = await _userService.RegisterUser(register);

            switch (res)
            {
                case RegisterUserResult.EmailExists:
                    return JsonResponseStatus.Error(new { info = "EmailExist" });
            }

            return JsonResponseStatus.Success();






        }



        #endregion
        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.LoginUser(login);

                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound(new { message = "کاربری با مشخصات وارد شده یافت نشد" });

                    //case LoginUserResult.NotActivated:
                    //    return JsonResponseStatus.Error(new { message = "حساب کاربری شما فعال نشده است" });

                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email); // Ensure this method works correctly and returns the user.

                        if (user == null)
                        {
                            return JsonResponseStatus.NotFound(new { message = "User not found" });
                        }

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEshopJwtBearer"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:44337",
                            claims: new List<Claim>
                            {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                            },
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signinCredentials
                        );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                        return JsonResponseStatus.Success(new
                        {
                            token = tokenString,
                            expireTime = 30,
                            email = user.Email,
                            userName = user.UserName,
                            userId = user.Id
                        });
                }
            }

            return JsonResponseStatus.Error(new { message = "Invalid model state" });
        }

        #endregion
        #region SignOut

        #endregion
    }
}
