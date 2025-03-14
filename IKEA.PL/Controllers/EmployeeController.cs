using IKEA.BLL.Models;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services.Employees;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _Logger;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
            _Logger = logger;
        }
        #endregion
        #region Index
        [HttpGet]
        //BaseUrl/Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = _employeeService.CreateEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Sorry The Employee Has not Been Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                //1- log Exeption
                _Logger.LogError(ex, ex.Message);
                //2- Set Frindly Message
                if (_webHostEnvironment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employee);
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
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }
        #endregion
        #region Edit
        #region Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();//404
            var viewModel = new UpdatedEmployeeDto()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                Salary=employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
            };

            return View(viewModel);

        }
        #endregion
        #region post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var message = string.Empty;
            try
            {
                var updated = _employeeService.UpdateEmployee(employee) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                message = "Sorry , An Error Occured While Updating The employee";

            }
            catch (Exception ex)
            {
                //1-Log Exception
                _Logger.LogError(ex, ex.Message);
                //2- Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry , An Error Occured While Updating The Department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }
        #endregion
        #endregion
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "An error occurred while deleting the department.";
            }
            catch (Exception ex)
            {
                //1- log Exception
                _Logger.LogError(ex, ex.Message);
                //2-Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An error occurred while deleting the department.";
            }

            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
