using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Dto;
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

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="employeeRepository">Репозиторий работников</param>
        /// <param name="logger">Инициализатор логгера</param>
        public EmployeeService(IEmployeeRepository employeeRepository, ILoggerFactory logger)
            : base(logger)
        {
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Получение всех работников
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            return ExecuteWithCatch(() =>
            {
                var employees = _employeeRepository.GetAllEmployees();
                return ServiceResponseGeneric<IEnumerable<EmployeeDto>>.Success(
                    employees.Select((employee) => new EmployeeDto(employee)));
            });
        }

        /// <summary>
        /// Получение объекта работника, у которого введенный логин
        /// </summary>
        /// <param name="login">Логин работника</param>
        public ServiceResponseGeneric<EmployeeDto> GetEmployeeByLogin(string login)
        {
            return ExecuteWithCatch(() =>
            {
                if (LoginIsTooLong(login))
                    return ServiceResponseGeneric<EmployeeDto>.Warning(LoginTooLong);
                var employee = _employeeRepository.GetEmployeeByLogin(login);
                return ServiceResponseGeneric<EmployeeDto>.Success(new EmployeeDto(employee));
            });
        }

        /// <summary>
        /// Регистрация нового работника
        /// </summary>
        /// <param name="employee">Объект нового работника</param>
        public ServiceResponse RegisterNewEmployee(EmployeeDto employee)
        {
            return ExecuteWithCatch(() =>
            {
                if (NameIsTooLong(employee.Name))
                    return ServiceResponse.Warning(NameTooLong);

                if (LoginIsTooLong(employee.Login))
                    return ServiceResponse.Warning(LoginTooLong);

                if (LoginIsAlreadyExists(employee.Login))
                    return ServiceResponse.Warning(LoginAlreadyExists);

                _employeeRepository.RegisterNewEmployee(new Employee(employee));
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Получение объекта работника по идентификатору
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponseGeneric<EmployeeDto> GetEmployeeById(int employeeId)
        {
            return ExecuteWithCatch(() =>
            {
                var employee = _employeeRepository.GetEmployeeById(employeeId);
                return ServiceResponseGeneric<EmployeeDto>.Success(new EmployeeDto(employee));
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
                _logger.LogWarning("Попытка добавить работника с именем длиной {0}", name.Length);
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
                _logger.LogWarning("Попытка добавить работника с логином длиной {0}", login.Length);
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
            if (_employeeRepository.GetEmployeeByLogin(login) != null)
            {
                _logger.LogWarning("Работник с логином '{0} уже существует'", login);
                return true;
            }
            return false;
        }
    }
}
