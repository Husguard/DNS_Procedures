using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
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
        /// <returns></returns>
        public Employee GetEmployeeByLogin(string login)
        {
            return _db.ExecuteReaderGetSingle<Employee>(
                "TaskProcedureGetEmployeeByLogin",
                EmployeeFromReader,
                new SqlParameter("@Login", login));
        }

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employee">Объект нового работника</param>
        public void RegisterNewEmployee(Employee employee)
        {
            _db.ExecuteNonQuery(
               "TaskProcedureRegisterEmployee",
               new SqlParameter("@Name", employee.Name),
               new SqlParameter("@Login", employee.Login)
               );
        }

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
