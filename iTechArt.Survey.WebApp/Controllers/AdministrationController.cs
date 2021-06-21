using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> EditUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var user = await _userService.FindUserByIdAsyncOrReturnNull(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PasswordHash = user.PasswordHash,
                Role = await _userService.GetUserRoleAsync(user),
                AllRoles = await _userService.GetAllRoles()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (model.Id == Guid.Empty)
            {
                return BadRequest();
            }

            var user = await _userService.FindUserByIdAsyncOrReturnNull(model.Id);
            if (user == null)
            {
                return NotFound();
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

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var user = await _userService.FindUserByIdAsyncOrReturnNull(id);
            if (user == null)
            {
                return NotFound();
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

        [HttpGet]
        public async Task<IActionResult> DisplayUsers(int? pageNumber, int? itemCountPerPage)
        {
            var itemCount = itemCountPerPage ?? 5;
            var page = pageNumber ?? 0;

            itemCount = itemCount switch
            {
                < 1 => 1,
                > 100 => 100,
                _ => itemCount
            };

            var usersTotalCount = await _userService.GetUsersTotalCountAsync();

            if (page < 0)
            {
                page = 0;
            }
            else if (page > Math.Ceiling((double)usersTotalCount / itemCount) - 1)
            {
                page = (int)Math.Ceiling((double)usersTotalCount / itemCount) - 1;
            }

            var model = new DisplayUsersViewModel()
            {
                ItemCountPerPage = itemCount,
                PageNumber = page,
                TotalCount = await _userService.GetUsersTotalCountAsync(),
                UsersInfo = await _userService.GetUsersInfoForPageAsync(page, itemCount)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult DisplayUsers(DisplayUsersViewModel model)
        {
            return RedirectToAction("DisplayUsers",new {pageNumber = model.PageNumber, itemCountPerPage = model.ItemCountPerPage});
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            var model = new AddUserViewModel()
            {
                AllRoles = await _userService.GetAllRoles()
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
                await _userService.AddUserRoleAndClaimAsync(user, model.Role);

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
