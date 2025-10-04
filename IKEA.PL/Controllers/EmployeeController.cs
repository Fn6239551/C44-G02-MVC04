using IKEA.BLL.DTOS.Department;
using IKEA.BLL.DTOS.Employee;
using IKEA.BLL.Services.Classess;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models.DepartmentModule;
using IKEA.DAL.Models.EmployeeModule;
using IKEA.PL.ViewModels.EmployeeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IKEA.PL.Controllers
{
    [Authorize]
    public class EmployeeController (IEmployeesService employeesService, ILogger<EmployeeController> logger, IWebHostEnvironment environment,IDepartmentService departmentService) : Controller
    {
        private readonly IEmployeesService _employeesService = employeesService;
        private readonly ILogger<EmployeeController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IDepartmentService _departmentService = departmentService;

        #region Index
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employee = _employeesService.GetAllEmployees(false);
            if (!string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                Employee=Employee.Where(e=>e.Name.Contains(EmployeeSearchName,StringComparison.OrdinalIgnoreCase));
            }
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
            var Model = new EmployeeViewModel
            {
                Departments = _departmentService.GetAllDepaartment()
                .Select(d=>new SelectListItem { Value=d.DeptId.ToString(),Text=d.Name}).ToList()
            };
            return View(Model);
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {
                        Name=EmployeeVM.Name,
                        Age=EmployeeVM.Age,
                        Address=EmployeeVM.Address,
                        Email=EmployeeVM.Email,
                        EmployeeType=EmployeeVM.EmployeeType,
                        Gender=EmployeeVM.Gender,
                        HiringDate=EmployeeVM.HiringDate,
                        IsActive=EmployeeVM.IsActive,
                        PhoneNumber=EmployeeVM.PhoneNumber,
                        Salary=EmployeeVM.Salary,
                        DepartmentId=EmployeeVM.DepartmentId,
                        Image =EmployeeVM.Image
                    };
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(E => E.Errors).Select(e => e.ErrorMessage).ToList();



                ViewBag.Errors = errors;
                EmployeeVM.Departments = _departmentService.GetAllDepaartment()
                  .Select(d => new SelectListItem { Value = d.DeptId.ToString(), Text = d.Name }).ToList() ;
                return View(EmployeeVM);
            }
            try
            {
                var employeeDto = new CreatedEmployeeDto()
                {
                    Name = EmployeeVM.Name,
                    Age = EmployeeVM.Age,
                    Address = EmployeeVM.Address,
                    Email = EmployeeVM.Email,
                    EmployeeType = EmployeeVM.EmployeeType,
                    Gender = EmployeeVM.Gender,
                    HiringDate = EmployeeVM.HiringDate,
                    IsActive = EmployeeVM.IsActive,
                    PhoneNumber = EmployeeVM.PhoneNumber,
                    Salary = EmployeeVM.Salary,
                    DepartmentId = EmployeeVM.DepartmentId,
                    Image = EmployeeVM.Image
                };

                int result = _employeesService.CreateEmployee(employeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Employee cannot be created");
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                else
                    _logger.LogError(ex.Message);
            }
            EmployeeVM.Departments = _departmentService.GetAllDepaartment()
       .Select(d => new SelectListItem { Value = d.DeptId.ToString(), Text = d.Name }).ToList();

            return View(EmployeeVM);
        }

        
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeesService.GetEmployeeById(id.Value);
            if (employee is null) return NotFound();

            var EmployeeVM = new EmployeeViewModel()
            {
               
                Name = employee.Name,
                Email = employee.Email,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                Age = employee.Age,
                HiringDate = employee.HiringDate,
                Salary = employee.Salary,
                Gender = employee.EmpGender,
                EmployeeType = employee.EmpType,
                IsActive = employee.IsActive,
                DepartmentId = employee.DepartmentId
            };
            EmployeeVM.Departments =
                 _departmentService.GetAllDepaartment()
                .Select(d => new SelectListItem 
                {
                    Value = d.DeptId.ToString(),
                    Text = d.Name,
                    Selected=d.DeptId== employee.DepartmentId

                }).ToList();
            return View(EmployeeVM);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel EmployeeVM)
        {
            if (!id.HasValue) return BadRequest();
            if (!ModelState.IsValid)
            {
                EmployeeVM.Departments =
                                    _departmentService.GetAllDepaartment()
                                    .Select(d => new SelectListItem
                                    {
                                        Value = d.DeptId.ToString(),
                                        Text = d.Name,
                                        Selected = d.DeptId == EmployeeVM.DepartmentId
                                    });
                return View(EmployeeVM);
            }
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = EmployeeVM.Name,
                    Email = EmployeeVM.Email,
                    Address = EmployeeVM.Address,
                    PhoneNumber = EmployeeVM.PhoneNumber,
                    Age = EmployeeVM.Age,
                    HiringDate = EmployeeVM.HiringDate,
                    Salary = EmployeeVM.Salary,
                    Gender = EmployeeVM.Gender,
                    EmployeeType = EmployeeVM.EmployeeType,
                    IsActive = EmployeeVM.IsActive,
                    DepartmentId = EmployeeVM.DepartmentId
                };
                var Result = _employeesService.UpdateEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Employee Can not Be Update");
                EmployeeVM.Departments =
                                    _departmentService.GetAllDepaartment()
                                    .Select(d => new SelectListItem
                                    {
                                        Value = d.DeptId.ToString(),
                                        Text = d.Name,
                                        Selected = d.DeptId == EmployeeVM.DepartmentId
                                    }).ToList();
                return View(EmployeeVM);
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    EmployeeVM.Departments =
                           _departmentService.GetAllDepaartment()
                           .Select(d => new SelectListItem
                           {
                               Value = d.DeptId.ToString(),
                               Text = d.Name,
                               Selected = d.DeptId == EmployeeVM.DepartmentId
                           });
                    return View(EmployeeVM);
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
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
