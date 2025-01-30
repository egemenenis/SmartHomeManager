using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartHomeManager.Models;
using SmartHomeManager.ViewModels;
using System.Threading.Tasks;

namespace SmartHomeManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register() => View();
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Register", new { successMessage = "Registration successful! You are now logged in." });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return RedirectToAction("Register", new { errorMessage = "Registration failed! Please try again." });
            }

            return View(model);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", new { successMessage = "Login successful!" });
                    }
                    return RedirectToAction("Login", new { errorMessage = "Invalid login attempt." });
                }
                else
                {
                    return RedirectToAction("Login", new { errorMessage = "User not found." });
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", new { successMessage = "You have logged out successfully." });
        }
    }
}