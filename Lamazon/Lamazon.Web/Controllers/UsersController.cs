using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lamazon.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserCredentialsViewModel userCredentialsViewModel, string returnUrl)
        {
            // Validation is handled by FluentValidation
            if (!ModelState.IsValid)
            {
                return View(userCredentialsViewModel);
            }

            var userValidationResult = _usersService.ValidateUser(userCredentialsViewModel);

            if (userValidationResult == null)
            {
                ModelState.AddModelError("UserLoginError", "User not found");
                return View(userCredentialsViewModel);
            }
            else if (string.IsNullOrEmpty(userValidationResult.Email))
            {
                ModelState.AddModelError("UserLoginError", "Invalid email or password");
                return View(userCredentialsViewModel);
            }
            else
            {
                await AuthHelper.SignInUser(userValidationResult, HttpContext);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("/");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await AuthHelper.SignoutUser(HttpContext);
            return Redirect("/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            
            var registerResultUserViewModel = _usersService.RegisterUser(userViewModel);
            if (registerResultUserViewModel != null)
            {
                await AuthHelper.SignInUser(registerResultUserViewModel, HttpContext);
                return Redirect("/");
            }

            return View(userViewModel);
        }
    }
}
