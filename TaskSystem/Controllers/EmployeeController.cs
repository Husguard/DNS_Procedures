using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Controllers
{
    /// <summary>
    /// Контроллер работников
    /// </summary>
    public class EmployeeController : ControllerBase
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

        /// <summary>
        /// Получение всех работников
        /// </summary>
        [HttpGet]
        [Route("GetAllEmployees")]
        public ServiceResponseGeneric<IEnumerable<EmployeeDto>> GetAllEmployees() => _employeeService.GetAllEmployees();

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employeeDto">Объект нового работника</param>
        [HttpPost]
        [Route("RegisterNewEmployee")]
        public ServiceResponse RegisterNewEmployee(EmployeeDto employeeDto) => _employeeService.RegisterNewEmployee(employeeDto);

        /// <summary>
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        [HttpGet]
        [Route("GetEmployeeByLogin")]
        public ServiceResponse GetEmployeeByLogin(string login) => _employeeService.GetEmployeeByLogin(login);
    }
}
