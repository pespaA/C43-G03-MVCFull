using IKEA.BLL.Models;
using IKEA.BLL.Services;
using IKEA.DAL.Models.Departments;
using IKEA.DAL.Presistance.Repositories.Departments;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _Logger;
        private readonly IWebHostEnvironment _environment;

        #region Services
        public DepartmentController(IDepartmentService departmentService,ILogger<DepartmentController> logger,IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _Logger = logger;
            _environment = environment;
        }
        #endregion
        #region Index
        [HttpGet]
        //BaseUrl/Department/Index
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);
            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry The Department Has not Been Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }
            }catch (Exception ex)
            {
                //1- log Exeption
                _Logger.LogError(ex, ex.Message);
                //2- Set Frindly Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Sorry The Department Has not Been Created";
                    return View("Error", message);
                }

            }
            
        }
        #endregion
        #endregion
        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();//404
            var viewModel = new DepartmentEditViewModel()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            };

            return View(viewModel);  

        }
        #endregion
        #region post
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            _Logger.LogInformation($"Editing department with ID: {id}, ViewModel ID: {departmentVM.Id}");
            var message = string.Empty;
            try
            {
                var updatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };
                var updated = _departmentService.UpdateDepartment(updatedDepartment)>0;
                if(updated)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry , An Error Occured While Updating The Department";

            }
            catch (Exception ex)
            {
                //1-Log Exception
                _Logger.LogError(ex, ex.Message);
                //2- Set Message
                message=_environment.IsDevelopment()?ex.Message: "Sorry , An Error Occured While Updating The Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion
        #endregion
        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound();

            return View(department); 
        }
        //-----------------
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            var message = string.Empty;
            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "An error occurred while deleting the department.";
            }
            catch (Exception ex)
            {
                //1- log Exception
                _Logger.LogError(ex, ex.Message);
                //2-Set Message
                message = _environment.IsDevelopment() ? ex.Message : "An error occurred while deleting the department.";
            }

            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index)); 
        }
        #endregion
    }
}
