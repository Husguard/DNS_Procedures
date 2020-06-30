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
        protected readonly UserManager _manager;

        public BaseService(ILoggerFactory logger, UserManager manager)
        {
            _manager = manager;
            _logger = logger.CreateLogger<BaseService>();
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        protected ServiceResponse<T> ExecuteWithCatch<T>(Func<ServiceResponse<T>> function)
        {
            try
            {
                return function();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return ServiceResponse<T>.Fail(ex);
            }
            catch (EmptyResultException ex)
            {
                return ServiceResponse<T>.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServiceResponse<T>.Fail(ex);
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
                _logger.LogError(ex.Message);
                return ServiceResponse.Fail(ex);
            }
            catch (EmptyResultException ex)
            {
                return ServiceResponse.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServiceResponse.Fail(ex);
            }
        }
    }
}
