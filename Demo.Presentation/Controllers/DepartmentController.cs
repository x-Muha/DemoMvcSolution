using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {
        public IActionResult Index()
        {
            var Departments = departmentService.GetAllDepartmetns();
            return View(Departments);
        }
    }
}
