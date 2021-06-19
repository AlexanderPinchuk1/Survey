using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iTechArt.Survey.Domain.Identity;
using iTechArt.Survey.Foundation;
using iTechArt.Survey.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace iTechArt.Survey.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly IUserService _userService;


        public AdministrationController(IUserService userService)
        {
           _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var isGuid = Guid.TryParse(id, out _ );
            if (!isGuid)
            {
                Response.StatusCode = 404;

                return View("Errors/UserNotFound", id);
            }

            var user = await _userService.FindUserByIdAsyncOrReturnNull(id);
            if (user == null)
            {
                Response.StatusCode = 404;

                return View("Errors/UserNotFound", id);
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PasswordHash = user.PasswordHash,
                Role = await _userService.GetUserRoleAsync(user),
                AllRoles = _userService.GetAllRoles()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userService.FindUserByIdAsyncOrReturnNull(Convert.ToString(model.Id));
            if (user == null)
            {
                return RedirectToAction("DisplayUsers");
            }
            
            user.Email = model.Email;
            user.PasswordHash = model.PasswordHash;

            var result = await _userService.EditUserAsync(user, model.Role, model.DisplayName);
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
            var isGuid = Guid.TryParse(id, out _);
            if (!isGuid)
            {
                Response.StatusCode = 404;

                return View("Errors/UserNotFound", id);
            }

            var user = await _userService.FindUserByIdAsyncOrReturnNull(id);
            if (user == null)
            {
                Response.StatusCode = 404;

                return View("Errors/UserNotFound", id);
            }

            var result = await _userService.DeleteUserAsync(user);
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

        public async Task<IActionResult> DisplayUsers(DisplayUsersViewModel model)
        {
            var numItemsPerPage = _userService.ValidateNumberOfItemsPerPage(model.NumItemsPerPage);

            var usersTotalCount =  await _userService.GetUsersTotalCountAsync();
            var numPages = _userService.ValidateNumberOfPages(model.NumPage, numItemsPerPage, usersTotalCount);
            
            var users = await _userService.GetUsersPagedListAsync(numPages, numItemsPerPage);

            var usersInfo = new List<UserInfoViewModel>();
            foreach (var user in users)
            {
                var role = await _userService.GetUserRoleAsync(user);

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

            model = new DisplayUsersViewModel()
            {
                UsersInfo = usersInfo,
                NumItemsPerPage = numItemsPerPage,
                NumPage = numPages,
                TotalCount = usersTotalCount
            };
            
            return View(model);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var model = new AddUserViewModel()
            {
                AllRoles = _userService.GetAllRoles()
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

            var result = await _userService.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userService.AddUserToRoleAsync(user, model.Role);
                await _userService.AddUserClaimAsync(user, new System.Security.Claims.Claim("DisplayName", user.DisplayName));

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
