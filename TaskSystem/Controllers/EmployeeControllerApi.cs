using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models.Dto;
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
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        [HttpGet("GetEmployeeByLogin")]
        public ServiceResponse GetEmployeeByLogin(string login) => _employeeService.GetEmployeeByLogin(login);
    }
}
