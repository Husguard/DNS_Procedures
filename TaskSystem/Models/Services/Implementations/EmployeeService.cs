using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Models.Services.Implementations
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с работниками
    /// </summary>
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private const string LoginTooLong = "Логин работника слишком длинный, ограничение в 100 символов";
        private const string NameTooLong = "Имя работника слишком длинное, ограничение в 100 символов";
        private const string LoginAlreadyExists = "Работник с таким логином уже существует";

        public EmployeeService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger)
            : base(taskRepository, employeeRepository, logger)
        {
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Получение всех работников
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<Employee>> GetAllEmployees()
        {
            return ExecuteWithCatch(() =>
            {
                var employees = _employeeRepository.GetAllEmployees();
                return ServiceResponseGeneric<IEnumerable<Employee>>.Success(employees);
            });
        }

        /// <summary>
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        public ServiceResponseGeneric<Employee> GetEmployeeByLogin(string login)
        {
            return ExecuteWithCatch<Employee>(() =>
            {
                if (LoginIsTooLong(login))
                    return ServiceResponseGeneric<Employee>.Warning(LoginTooLong);
                var employee = _employeeRepository.GetEmployeeByLogin(login);
                return ServiceResponseGeneric<Employee>.Success(employee);
            });
        }

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employee">Объект нового работника</param>
        public ServiceResponse RegisterNewEmployee(Employee employee)
        {
            return ExecuteWithCatch(() =>
            {
                if (NameIsTooLong(employee.Name))
                    return ServiceResponse.Warning(NameTooLong);
                if(LoginIsTooLong(employee.Login))
                    return ServiceResponse.Warning(LoginTooLong);
                if (LoginIsAlreadyExists(employee.Login))
                    return ServiceResponse.Warning(LoginAlreadyExists);
                _employeeRepository.RegisterNewEmployee(employee);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Получение объекта работника по идентификатору
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponseGeneric<Employee> GetEmployeeById(int employeeId)
        {
            return ExecuteWithCatch<Employee>(() =>
            {
                var employee = _employeeRepository.GetEmployeeById(employeeId);
                return ServiceResponseGeneric<Employee>.Success(employee);
            });
        }

        /// <summary>
        /// Метод проверки длины введенного имени работника
        /// </summary>
        /// <param name="name">Имя работника</param>
        private bool NameIsTooLong(string name)
        {
            if (name.Length > 100)
            {
                _logger.LogWarning("Employee Name with Length = {0} is incorrect", name.Length);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки длины введенного логина работника
        /// </summary>
        /// <param name="login">Логин работника</param>
        private bool LoginIsTooLong(string login)
        {
            if (login.Length > 100)
            {
                _logger.LogWarning("Employee Login with Length = {0} is incorrect", login.Length);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки существования работника с введенным логином
        /// </summary>
        /// <param name="login">Логин работника</param>
        private bool LoginIsAlreadyExists(string login)
        {
            if (_employeeRepository.GetEmployeeByLogin(login) == null)
            {
                _logger.LogWarning("Employee with Login = {0} is exists", login);
                return true;
            }
            return false;
        }
    }
}
