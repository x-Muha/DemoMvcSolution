using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels.UserManagerViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class UserManagerController(UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Index
        public IActionResult Index(string UserSearchName)
        {
            if (string.IsNullOrWhiteSpace(UserSearchName))
                return View(_userManager.Users
                .Select(u => new UserDetailsViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName!,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!
                }).ToList());

            var Users = _userManager.Users
                .Where(
                u => u.FirstName.Contains(UserSearchName) ||
                (u.Email != null && u.Email.Contains(UserSearchName)) ||
                (u.LastName != null && u.LastName.Contains(UserSearchName)) ||
                (u.UserName != null && u.UserName.Contains(UserSearchName))
                ).Select(u => new UserDetailsViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName!,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!
                })
                .ToList();
            return View(Users);
        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if(user == null) return NotFound();
            var viewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!
            };
            return View(viewModel);

        }

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            var user = _userManager.FindByIdAsync(id!).Result;
            if (user == null) return NotFound();
            var viewModel = new UserEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName!,
                PhoneNumber = user.PhoneNumber!
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(UserEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            if (string.IsNullOrEmpty(viewModel.Id)) return BadRequest();
            var user = _userManager.FindByIdAsync(viewModel.Id).Result;
            if (user == null) return NotFound();

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.PhoneNumber = viewModel.PhoneNumber;
            
            var result = _userManager.UpdateAsync(user).Result;
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(viewModel);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete

        [HttpGet]
        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null) return NotFound();
            var viewModel = new UserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName!,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!
            };
            return View(viewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string? id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null) return NotFound();
            _userManager.DeleteAsync(user).Wait();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
