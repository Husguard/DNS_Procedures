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

        /// <summary>
        /// Конвертация из модели данных в бизнес-модель
        /// </summary>
        public Employee(EmployeeDto employeeDto)
        {
            Name = employeeDto.Name;
            Login = employeeDto.Login;
        }

        /// <summary>
        /// Пустой конструктор для создания объекта в потоке БД
        /// </summary>
        public Employee() {}
    }
}
