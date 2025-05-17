using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
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

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeeDetails(id.Value);
            if(employee is null) return NotFound();
            var employeeDTO = new UpdatedEmployeeDTO()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeDTO);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id, UpdatedEmployeeDTO employeeDTO)
        {
            if(!id.HasValue || id.Value != employeeDTO.Id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeDTO);
            try
            {
                var Result = _employeeService.UpdateEmployee(employeeDTO);
                if (Result > 0) return RedirectToAction(nameof(Index));
                ModelState.AddModelError(string.Empty, "Emp Not Updated");  
            }
            catch (Exception e)
            {
                if (environment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, e.Message);
                else Logger.LogError(e.Message);
                return View("ErrorView", e);
            }
            return View(employeeDTO);
            
        }

    }
}
