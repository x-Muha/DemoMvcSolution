using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        // BaseUrl/Departments/Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartmetns();
            return View(departments);
        }
    }
}
