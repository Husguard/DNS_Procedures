using System.Collections.Generic;
using TaskSystem.Models.Dto;

namespace TaskSystem.Models.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса взаимодействия с работниками
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Получение всех работников
        /// </summary>
        public ServiceResponse<IEnumerable<EmployeeDto>> GetAllEmployees();

        /// <summary>
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        public ServiceResponse<EmployeeDto> GetEmployeeByLogin(string login);

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employee">Объект нового работника</param>
        public ServiceResponse RegisterNewEmployee(LoginEmployee employee);

        /// <summary>
        /// Получение объекта работника по идентификатору
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponse<EmployeeDto> GetEmployeeById(int employeeId);

    }
}
