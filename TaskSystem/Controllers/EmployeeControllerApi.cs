using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Controllers
{
    /// <summary>
    /// Контроллер работников
    /// </summary>
    [ApiController]
    public class EmployeeControllerApi : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="employeeService">Сервис работников</param>
        public EmployeeControllerApi(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Получение всех работников
        /// </summary>
        [HttpGet("GetAllEmployees")]
        public ServiceResponse<IEnumerable<EmployeeDto>> GetAllEmployees() => _employeeService.GetAllEmployees();

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employeeDto">Объект нового работника</param>
        [HttpPost("RegisterNewEmployee")]
        public ServiceResponse RegisterNewEmployee([FromBody] LoginEmployee employeeDto) => _employeeService.RegisterNewEmployee(employeeDto);

        /// <summary>
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        [HttpGet("GetEmployeeByLogin")]
        public ServiceResponse GetEmployeeByLogin(string login) => _employeeService.GetEmployeeByLogin(login);
    }
}
