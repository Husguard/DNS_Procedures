using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Dto;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Модель работника
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Идентификатор работника
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя работника
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уникальный логин работника
        /// </summary>
        public string Login { get; set; }

        public Employee(EmployeeDto employeeDto)
        {
            Name = employeeDto.Name;
            Login = employeeDto.Login;
        }
        public Employee() {}
    }
}
