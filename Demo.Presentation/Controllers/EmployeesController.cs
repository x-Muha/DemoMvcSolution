using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, 
        IWebHostEnvironment environment, ILogger<EmployeesController> Logger) : Controller
    {
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(CreatedEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Result = _employeeService.AddEmployee(employeeDTO);
                    if (Result > 0) return RedirectToAction("Index"); //Added
                    else ModelState.AddModelError(string.Empty, "Not Added");
                }
                catch (Exception e)
                {
                    if(environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty,e.Message);
                    else Logger.LogError(e.Message);
                }
            }
            return View(employeeDTO);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeDetails(id.Value);
            return employee is null? NotFound() : View(employee);
        }
    }
}
