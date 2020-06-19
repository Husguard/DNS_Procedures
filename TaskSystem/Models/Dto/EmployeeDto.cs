using System.Runtime.Serialization;
using TaskSystem.Models.Objects;

namespace TaskSystem.Dto
{
    /// <summary>
    /// Модель DTO для работника
    /// </summary>
    [DataContract]
    public class EmployeeDto
    {
        /// <summary>
        /// Идентификатор работника
        /// </summary>
        [DataMember(Name = "employeeId")]
        public int Id { get; set; }

        /// <summary>
        /// Имя работника
        /// </summary>
        [DataMember(Name = "employeeName")]
        public string Name { get; set; }

        /// <summary>
        /// Уникальный логин работника
        /// </summary>
        [DataMember(Name = "login")]
        public string Login { get; set; }

        /// <summary>
        /// Конвертация модели бизнес-логики в модель данных
        /// </summary>
        /// <param name="employee">Объект работника</param>
        public EmployeeDto(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Login = employee.Login;
        }

        /// <summary>
        /// Пустой конструктор для принятия объекта со стороны клиента
        /// </summary>
        public EmployeeDto() { }
    }
}
