using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register() => View();
    }
}
