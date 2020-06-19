using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects.Repositories
{
    /// <summary>
    /// Репозиторий для получения и добавления работников
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConnectionDb _db;
        /// <summary>
        /// Агрегация класса подключения к БД
        /// </summary>
        /// <param name="db">Класс подключения к БД</param>
        public EmployeeRepository(IConnectionDb db)
        {
            _db = db;
        }

        /// <summary>
        /// Получение списка всех работников
        /// </summary>
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _db.ExecuteReaderGetList(
                "TaskProcedureGetAllEmployees",
                EmployeeFromReader);
        }

        /// <summary>
        /// Получение работника по логину
        /// </summary>
        /// <param name="login">Логин работника</param>
        public Employee GetEmployeeByLogin(string login)
        {
            return _db.ExecuteReaderGetSingle<Employee>(
                "TaskProcedureGetEmployeeByLogin",
                EmployeeFromReader,
                new SqlParameter("@Login", login));
        }

        /// <summary>
        /// Получение работника по идентификатору
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public Employee GetEmployeeById(int employeeId)
        {
            return _db.ExecuteReaderGetSingle<Employee>(
                "TaskProcedureGetEmployeeByID",
                EmployeeFromReader,
                new SqlParameter("@EmployeeID", employeeId));
        }

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employee">Объект нового работника</param>
        public void RegisterNewEmployee(Employee employee)
        {
            _db.ExecuteNonQuery(
               "TaskProcedureAddEmployee",
               new SqlParameter("@Name", employee.Name),
               new SqlParameter("@Login", employee.Login)
               );
        }

        /// <summary>
        /// Метод создания объекта работника из потока данных
        /// </summary>
        /// <param name="reader">Класс чтения потока данных из БД</param>
        public Employee EmployeeFromReader(IDataReader reader)
        {
            return new Employee()
            {
                Id = (int)reader["ID"],
                Name = (string)reader["Name"],
                Login = (string)reader["Login"]
            };
        }
    }
}
