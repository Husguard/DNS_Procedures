using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Services
{
    public abstract class BaseService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        protected readonly ILogger<BaseService> _logger;
        protected int _currentUser;

        protected const string WorkTaskNotFound = "Задание не было найдено";
        protected const string EmployeeNotFound = "Работник не найден";
        public BaseService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger)
        {
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
            _currentUser = 1;
            _logger = logger.CreateLogger<BaseService>();
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        protected ServiceResponseGeneric<T> ExecuteWithCatch<T>(Func<ServiceResponseGeneric<T>> function)
        {
            try
            {
                return function();
            }
            catch (SqlException ex)
            {
                return ServiceResponseGeneric<T>.Fail(ex);
            }
            catch (EmptyResultException ex)
            {
                return ServiceResponseGeneric<T>.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                return ServiceResponseGeneric<T>.Fail(ex);
            }
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        protected ServiceResponse ExecuteWithCatch(Func<ServiceResponse> function)
        {
            try
            {
                return function();
            }
            catch (SqlException ex)
            {
                return ServiceResponse.Fail(ex);
            }
            catch(EmptyResultException ex)
            {
                return ServiceResponse.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex);
            }
        }

        /// <summary>
        /// Метод проверки существования задания с введенным идентификатором
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        protected bool TaskIsNotExists(int taskId)
        {
            if (!_taskRepository.GetTaskByID(taskId).Any())
            {
                _logger.LogWarning("Task with ID = {0} is not exists", taskId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки существования работника с введеным идентификатором
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        protected bool EmployeeIsNotExists(int employeeId)
        {
            if (_employeeRepository.GetEmployeeById(employeeId) == null)
            {
                _logger.LogWarning("Employee with ID = {0} is not exists", employeeId);
                return true;
            }
            return false;
        }
    }
}
