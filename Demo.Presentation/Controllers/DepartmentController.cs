using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger, IWebHostEnvironment _environment) : Controller
    {
        // BaseUrl/Departments/Index ->Default
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartmetns();
            return View(departments); //3rd overload that takes a
        }                             //model and render its data

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO departmentDTO)
        {
            //ModelState is a Inherited property from Controller
            if (ModelState.IsValid)//Server Side Validation
            {
                try
                {
                    int result = _departmentService.AddDepartment(departmentDTO);
                    //Data base insertion validation
                    if (result > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Dept not added!");
                    //return view named Create as action name
                        return View(departmentDTO);
                    //but will direct to View of "Get" Create
                    }
                }
                catch(Exception e)
                {
                    // Log Exception
                    if(_environment.IsDevelopment())
                    {// 1. Development => Log Error in Console and Return Same View 
                        ModelState.AddModelError(string.Empty, e.Message);
                    }
                    else
                    {// 2. Deployment => Log Error In File|Table in Db, Return error View
                        _logger.LogError(e.Message);
                    }
                }
            }
            // default return
            return View(departmentDTO);
        }
        #endregion

        #region Details of Department
        [HttpGet]
        public IActionResult Details(int? id) //nullable because Id field is optional in routing
        {
            if(!id.HasValue) return BadRequest();// 400 error - if the id doesn't match a department in database
            //using Id.Value because it's nullable type
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();// 404 error - if someone link manually written with no id
            return View(department);//return to Details View and it renders department model sent
        }
        #endregion
    }
}
