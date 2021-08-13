using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace iTechArt.Survey.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountController> _logger;
        
        public AccountController(SignInManager<User> signInManager, 
                                 ILogger<AccountController> logger,
                                 UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "User with entered email and password was not found!");

                return View(model);
            }
       
            _logger.LogInformation("User logged in.");

            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var user = new User
            {
                DisplayName = model.DisplayName, 
                Email = model.Email, 
                UserName = model.Email , 
                RegistrationDateTime = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);
         
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Common.Role.User);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("DisplayName", user.DisplayName));
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("About","Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction("Login", "Account");
        }
    }
}
