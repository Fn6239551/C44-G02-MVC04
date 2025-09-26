using IKEA.BLL.DTOS.Department;
using IKEA.BLL.Services.Interfaces;
using IKEA.DAL.Models;
using IKEA.PL.ViewModels.DepartmentViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace IKEA.PL.Controllers
{
    public class DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly ILogger<DepartmentController> _logger = logger;
        private readonly IWebHostEnvironment _environment = environment;

        [HttpGet]
        public IActionResult Index()
        {
            #region ViewData & ViewBag
            //   ViewData["Message"] = "Hello From View Data";
            //  ViewBag.Message = "Hello From View Bag";
            ViewData["Message"] = new DepartmentDto() { Name = "TestViewData" };
            ViewBag.Message = new DepartmentDto() { Name = "TestViewBag" }; 
            #endregion
            var Departments = _departmentService.GetAllDepaartment();
            return View(Departments);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name=viewModel.Name,
                        Code=viewModel.Code,
                        CreatedOn = viewModel.CreatedOn,
                        Description=viewModel.Description

                    };
                    int Result = _departmentService.AddDepartment(departmentDto);
                    string Message;
                    if (Result == 0) {
                        Message = $"Department:{viewModel.Name}Has Been Created";
                    }
                    else
                    {
                        Message = $"Department:{viewModel.Name}Has Not Been Created";
                    }
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    //Log Exception
                    //1-Development
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        // return View(departmentDto);
                    }
                    //2-Deployment
                    else
                    {
                        _logger.LogError(ex.Message);
                        //  return View(departmentDto);
                    }
                }
            }

            return View(viewModel);

        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();//400
            var Department = _departmentService.GetDartmentById(id.Value);
            if (Department is null) return NotFound();
            return View(Department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();//400
            var Department = _departmentService.GetDartmentById(id.Value);
            if (Department is null) return NotFound();
            var DepartmentViewModel = new DepartmentViewModel()
            {
                Code = Department.Code,
                Name = Department.Name,
                Description = Department.Description,
                CreatedOn = Department.CreatedOn
            };
            return View(DepartmentViewModel);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var UpdatedDepartment = new UpdateDepartmentDto()
                {
                    Id = id.Value,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    CreatedOn = viewModel.CreatedOn
                };
                int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can not Be Updated");
                    return View(viewModel);
                }

            }
            catch (Exception ex)
            {
                //Log Exception
                //1-Development
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                //2-Deployment
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }




        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();//400
        //    var Department = _departmentService.GetDartmentById(id.Value);
        //    if (Department is null) return NotFound();
        //    return View(Department);
        //}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department Can not Be Deleted");
                    return RedirectToAction(nameof(Delete),new { id });
                }
            }
            catch (Exception ex)
            {
                //Log Exception
                //1-Development
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //return View(viewModel);
                    return RedirectToAction(nameof(Index));
                }
                //2-Deployment
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }

            #endregion

        }
    }
}
