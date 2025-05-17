using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDTOs;
using Demo.BusinessLogic.Services;
using Demo.Presentation.Views.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

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
                catch (Exception e)
                {
                    // Log Exception
                    if (_environment.IsDevelopment())
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
            if (!id.HasValue) return BadRequest();// 400 error - if the id doesn't match a department in database
            //using Id.Value because it's nullable type
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();// 404 error - if someone link manually written with no id
            return View(department);//return to Details View and it renders department model sent
        }
        #endregion

        #region Edit Department

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            // Manual Mapping DeptDetailsDTO to DeptEditViewModel
            // No Need To Create factory, will use automapper in future
            var departmentViewModel = new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateofCreation = department.CreatedOn
            };
            return View(departmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, DepartmentEditViewModel viewModel)
        {       //From Route to prevent HTML injection <intput asp-for="Id">
                //Because it will inject in FORM which has higher priority than Route
            if (ModelState.IsValid)
            {
                try
                {
                    // Manual Mapping DeptEditViewModel to UpdatedDeptDTO
                    var UpdatedDepartment = new UpdatedDepartmentDTO()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateofCreation
                    };
                    int result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (result > 0) return RedirectToAction(nameof(Index));
                    else
                    {   // not recieved yet
                        ModelState.AddModelError(string.Empty, "Dept Not Updated !");
                    }
                }
                catch (Exception e) //Error inside Database while updating
                {
                    // Log Exception
                    if (_environment.IsDevelopment())
                    {// 1. Development => Log Error in Console and Return Same View 
                        ModelState.AddModelError(string.Empty, e.Message);
                    }
                    else
                    {// 2. Deployment => Log Error In File|Table in Db, Return error View
                        _logger.LogError(e.Message);
                        return View("ErrorView", e);
                    }
                }
            }
            return View(viewModel);
        }

        #endregion

        #region Delete Department

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department == null) return NotFound();
        //    return View(department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Dept is Not Deleted!");
                    // redirect to Delete and sending id = this.id 
                    return RedirectToAction(nameof(Delete), new{id});
                }
            }
            catch (Exception e)
            {
                // Log Exception
                if (_environment.IsDevelopment())
                {// 1. Development => Log Error in Console and Return Same View 
                    ModelState.AddModelError(string.Empty, e.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {// 2. Deployment => Log Error In File|Table in Db, Return error View
                    _logger.LogError(e.Message);
                    return View("ErrorView", e);
                }
            }
        }

        #endregion
    }
}
