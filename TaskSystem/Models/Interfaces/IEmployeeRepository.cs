using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для работников
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Получение списка всех работников
        /// </summary>
        IEnumerable<Employee> GetAllEmployees();

       /// <summary>
       /// Получение работника по логину
       /// </summary>
       /// <param name="login">Логин работника</param>
        Employee GetEmployeeByLogin(string login);

        /// <summary>
        /// Зарегистрировать нового работника
        /// </summary>
        /// <param name="employee">Объект работника</param>
        void RegisterNewEmployee(Employee employee);
    }
}
