using IKEA.BLL.DTOS.Department;
using IKEA.BLL.DTOS.Employee;
using IKEA.BLL.Services.Classess;
using IKEA.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController (IEmployeesService employeesService, ILogger<EmployeeController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly IEmployeesService _employeesService = employeesService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment; 

        #region Index
        public IActionResult Index()
        {
            var Employee = _employeesService.GetAllEmployees(false);
            return View(Employee);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeesService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeesService.CreateEmployee(employeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "Employee cannot be created");
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                        ModelState.AddModelError(string.Empty, ex.Message);
                    else
                        _logger.LogError(ex.Message);
                }
            }
            return View(employeeDto);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeesService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var employeeViewModel = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Age = employee.Age,
                HiringDate = employee.HiringDate,
                Salary = employee.Salary,
                Gender = employee.EmpGender,
                EmployeeType = employee.EmpType,
                IsActive = employee.IsActive
            };

            return View(employeeViewModel);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdatedEmployeeDto viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var updatedEmployee = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Address = viewModel.Address,
                    PhoneNumber = viewModel.PhoneNumber,
                    Age = viewModel.Age,
                    HiringDate = viewModel.HiringDate,
                    Salary = viewModel.Salary,
                    Gender = viewModel.Gender,
                    EmployeeType = viewModel.EmployeeType,
                    IsActive = viewModel.IsActive
                };

                int result = _employeesService.UpdateEmployee(updatedEmployee);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee cannot be updated");
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    _logger.LogError(ex.Message);

                return View(viewModel);
            }
        }
        #endregion

        #region Delete
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                var deleted = _employeesService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee cannot be deleted");
                    return RedirectToAction(nameof(Delete), new { id = id});
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                   // ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion
    }
}
