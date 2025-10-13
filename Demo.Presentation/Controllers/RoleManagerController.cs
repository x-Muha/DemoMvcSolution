using System.Linq;
using Demo.Presentation.ViewModels.RolerManagerViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class RoleManagerController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Index(string? RoleSearchName)
        {
            if (string.IsNullOrWhiteSpace(RoleSearchName))
                return View(_roleManager.Roles
                .Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name!
                })
                .ToList());
            return View(_roleManager.Roles
                .Where(r=> r.NormalizedName!.Contains(RoleSearchName))
                .Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name!
                })
                .ToList());
        }

        [HttpPost]
        public IActionResult Create(string? roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return BadRequest();
            _roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(string? Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var role = _roleManager.FindByIdAsync(Id).Result;
            if (role == null) return NotFound();
            var viewModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name!
            };
            return View(viewModel);

        }
        [HttpGet]
        public IActionResult Edit(string? Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var role = _roleManager.FindByIdAsync(Id).Result;
            if (role == null) return NotFound();
            var viewModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name!
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(RoleViewModel viewModel)
        {
            if (viewModel == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var role = _roleManager.FindByIdAsync(viewModel.Id).Result;
            if (role == null) return NotFound();
            role.Name= viewModel.RoleName;
            var result = _roleManager.UpdateAsync(role).Result;
            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }
        #region Delete
        [HttpGet]
        public IActionResult Delete(string? Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var role = _roleManager.FindByIdAsync(Id).Result;
            if (role == null) return NotFound();
            var viewModel = new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name!
            };
            return View(viewModel);

        }
        [HttpPost]
        public IActionResult Delete(RoleViewModel model)
        {
            if (model == null) return BadRequest();
            var role = _roleManager.FindByIdAsync(model.Id).Result;
            if (role == null) return NotFound();
            var result = _roleManager.DeleteAsync(role).Result;

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
