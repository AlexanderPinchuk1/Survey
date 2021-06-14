using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using X.PagedList;

namespace iTechArt.Survey.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;


        public AdministrationController(UserManager<User> userManager,
                                        RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
            {
                return RedirectToAction("DisplayUsers");
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PasswordHash = user.PasswordHash,
                Role = (await _userManager.GetRolesAsync(user)).First(),
                AllRoles = _roleManager.Roles.Select(role => role.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(Convert.ToString(model.Id));

            if (user == null)
            {
                return RedirectToAction("DisplayUsers");
            }
            
            user.Email = model.Email;
            user.PasswordHash = model.PasswordHash;

            if (! await _userManager.IsInRoleAsync(user, model.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
                await _userManager.AddToRoleAsync(user, model.Role);
            }

            if (model.DisplayName != user.DisplayName)
            {
                user.DisplayName = model.DisplayName;

                await _userManager.ReplaceClaimAsync(user, (await _userManager.GetClaimsAsync(user)).First(claim => claim.Type == "DisplayName"),
                    new System.Security.Claims.Claim("DisplayName", user.DisplayName));
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("DisplayUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("DisplayUsers");
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("DisplayUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("DisplayUsers");
        }

        public async Task<IActionResult> DisplayUsers(int? page)
        {
            var usersInfo = new List<UserInfoViewModel>();
            var users = _userManager.Users.Where(user => user.UserName != User.Identity.Name).ToList();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.First();

                usersInfo.Add(new UserInfoViewModel()
                {
                    Id = user.Id,
                    Role = role,
                    DisplayName = user.DisplayName,
                    RegistrationDateTime = user.RegistrationDateTime,
                    CreatedSurveys = 0,
                    CompletedSurveys = 0,
                });
            }

            const int pageSize = 5;
            var pageNumber = (page ?? 1);

            return View(usersInfo.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var model = new AddUserViewModel()
            {
                AllRoles = _roleManager.Roles.Select(role => role.Name).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            var user = new User
            {
                DisplayName = model.DisplayName, 
                Email = model.Email, 
                UserName = model.Email,
                RegistrationDateTime = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("DisplayName", user.DisplayName));

                return RedirectToAction("DisplayUsers", "Administration");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }
    }
}
