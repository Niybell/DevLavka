using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevLavka.Models;
using DevLavka.Models.Frontend.ArgsModels;
using DevLavka.Models.Frontend.ResponseModels;
using DevLavka.Models.Services.ResponseModels;
using DevLavka.Services.Interfaces;
using DevLavka.Utils.Validators.AuthController;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevLavka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _singInManager;
        private IUsersService _usersService;

        public UsersController(UserManager<User> userManager, SignInManager<User> singInManager, IUsersService userService)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _usersService = userService;
        }

        [HttpPost("register")]
        public async Task<TextServerResponse> Register(RegisterModel model)
        {
            RegisterModelValidator validator = new RegisterModelValidator(model);

            TextServerResponse validateResponse = validator.IsValidate(_userManager.Users);

            if (validateResponse.StatusCode == 1)
                return new TextServerResponse(validateResponse.Description, 500);

            CreateUserResponse createUserResponse = await _usersService.CreateUserAsync(model, _userManager);

            if (createUserResponse.StatusCode == 200)
                await _singInManager.SignInAsync(createUserResponse.CreatedUser, false);
            else
                return new TextServerResponse(createUserResponse.Description, 500);

            return new TextServerResponse("Successfully register", 200);
        }
        [HttpGet("logout")]
        public async Task<TextServerResponse> Logout()
        {
            await _singInManager.SignOutAsync();
            return new TextServerResponse("Successfully logout", 200);
        }

        [HttpPost("login")]
        public async Task<TextServerResponse> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var loginResult = await _singInManager.PasswordSignInAsync(user.Name, model.Password, model.RememberMe, false);

            if (loginResult.Succeeded)
                return new TextServerResponse("Successfully login", 200);
            else
                return new TextServerResponse("Login or password is incorrect", 500);
        }
        [HttpGet("get-account-date")]
        public async Task<GetAccountDateResponse> GetAccountDate()
        {
            User? user = await _userManager.GetUserAsync(HttpContext.User);

            GetAccountDateUser? getAccountDateUser = null;
            if (user != null)
            {
                getAccountDateUser = new GetAccountDateUser(
                    user.Name, user.Role
                );
            }

            return new GetAccountDateResponse(getAccountDateUser, 200);
        }
    }
}