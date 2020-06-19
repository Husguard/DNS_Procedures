using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="employeeService">Сервис работников</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult EmployeeList()
        {
            return View(_employeeService.GetAllEmployees().Result);
        }

        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterEmployee(EmployeeDto employeeDto)
        {
            _employeeService.RegisterNewEmployee(employeeDto);
            return RedirectToAction("EmployeeList");
        }
    }
}
