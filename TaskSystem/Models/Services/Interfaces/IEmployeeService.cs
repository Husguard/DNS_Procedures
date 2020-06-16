using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

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
        public ServiceResponseGeneric<IEnumerable<Employee>> GetAllEmployees();

        /// <summary>
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        public ServiceResponseGeneric<Employee> GetEmployeeByLogin(string login);

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employee">Объект нового работника</param>
        public ServiceResponse RegisterNewEmployee(Employee employee);

        /// <summary>
        /// Получение объекта работника по идентификатору
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponseGeneric<Employee> GetEmployeeById(int employeeId);

    }
}
