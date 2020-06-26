using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using TaskSystem.Dto;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Абстрактный класс сервисов
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// Общий логгер для всех сервисов и идентификатор текущего пользователя
        /// </summary>
        protected readonly ILogger<BaseService> _logger;
        protected UserManager _manager;

        protected const string WorkTaskNotFound = "Задание не было найдено";
        protected const string EmployeeNotFound = "Работник не найден";
        public BaseService(ILoggerFactory logger, UserManager manager)
        {
            _manager = manager;
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
            catch (EmptyResultException ex)
            {
                return ServiceResponse.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                return ServiceResponse.Fail(ex);
            }
        }
    }
}
