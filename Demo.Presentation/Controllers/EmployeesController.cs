using Demo.BusinessLogic.DataTransferObjects.EmployeeDTOs;
using Demo.BusinessLogic.Services;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModel;
using Demo.DataAccess.Models.Shared.Enums;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, 
        IWebHostEnvironment environment, ILogger<EmployeesController> Logger) : Controller
    { 
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(EmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updateEmp = new CreatedEmployeeDTO()
                    {
                        Name = viewModel.Name,
                        Salary = viewModel.Salary,
                        Address = viewModel.Address,
                        Age = viewModel.Age,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber,
                        IsActive = viewModel.IsActive,
                        EmployeeType = viewModel.EmployeeType,
                        Gender = viewModel.Gender,
                        HiringDate = viewModel.HiringDate,
                        DepartmentId = viewModel.DepartmentId,
                        Image = viewModel.Image
                    };



                    int Result = _employeeService.AddEmployee(updateEmp);
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
            return View(viewModel);
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
            var viewModel = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int? id, EmployeeViewModel viewModel)
        {
            if(!id.HasValue || id.Value != viewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var updateEmp = new UpdatedEmployeeDTO()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Salary = viewModel.Salary,
                    Address = viewModel.Address,
                    Age = viewModel.Age,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    IsActive = viewModel.IsActive,
                    EmployeeType = viewModel.EmployeeType,
                    Gender = viewModel.Gender,
                    HiringDate = viewModel.HiringDate,
                    DepartmentId = viewModel.DepartmentId,
                    Image = viewModel.Image
                };


                var Result = _employeeService.UpdateEmployee(updateEmp);
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
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id==0) return BadRequest();
            try
            {
                bool Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted) return RedirectToAction(nameof(Index));
                ModelState.AddModelError(string.Empty, "Emp Not Deleted");
                return RedirectToAction(nameof(Delete), new { id });
            }
            catch (Exception e)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return RedirectToAction(nameof(Index));
                }
                else Logger.LogError(e.Message);
                return View("ErrorView", e);
            }    
        }
    }
}
