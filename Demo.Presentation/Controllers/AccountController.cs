using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,
               SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var user = new ApplicationUser
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                Email = viewModel.Email,

            };
            var Result = _userManager.CreateAsync(user, viewModel.Password).Result;
            if (Result.Succeeded) return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(viewModel);
            }
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(LoginViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);
            var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if(user != null)
            {
                bool flag = _userManager.CheckPasswordAsync(user, viewModel.Password).Result;
                if(flag)
                {
                    var result = _signInManager.PasswordSignInAsync(user, viewModel.Password,
                                                         viewModel.RememberMe, false).Result;
                    if (result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account is not confirmed");
                    if (result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is Locked");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else   ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(viewModel); 
        }
        #endregion
        #region LogOut
        [HttpGet]
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }
        #endregion
        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword() => View();
        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user != null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var resetPasswordLink = Url.Action
                        ("ResetPassword","Account",new {email=viewModel.Email, Token},Request.Scheme);
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = resetPasswordLink = null!
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            else ModelState.AddModelError(string.Empty, "Invalid Operation");
            return View(nameof(ForgetPassword), viewModel);

        }
        [HttpGet]
        public IActionResult CheckYourInbox() => View();

        [HttpGet]
        public IActionResult ResetPassword(string email, string Token)
        {
            TempData["email"] = email;
            TempData["Token"] = Token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid) return View();
            string email = TempData["email"] as string ?? string.Empty;
            string Token = TempData["Token"] as string ?? string.Empty;
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user is not null)
            {
                var result =_userManager.ResetPasswordAsync(user, Token, viewModel.Password).Result;
                if (result.Succeeded) 
                    return RedirectToAction(nameof(Login));
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(viewModel);
        }

        #endregion
    }
}
 