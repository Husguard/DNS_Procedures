using Microsoft.Extensions.Logging;
using System;

namespace TaskSystem.Models.Services.Implementations
{
    /// <summary>
    /// Базовый класс сервисов
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// Общий логгер для всех сервисов и идентификатор текущего пользователя
        /// </summary>
        internal readonly ILogger<BaseService> _logger;
        internal readonly UserManager Manager;

        public BaseService(ILoggerFactory logger, UserManager manager)
        {
            Manager = manager;
            _logger = logger.CreateLogger<BaseService>();
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        public ServiceResponse<T> ExecuteWithCatch<T>(Func<ServiceResponse<T>> function)
        {
            try
            {
                return function();
            }
            catch (EmptyResultException ex)
            {
                return ServiceResponse<T>.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServiceResponse<T>.Critical(ex);
            }
        }

        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        public ServiceResponse ExecuteWithCatch(Func<ServiceResponse> function)
        {
            try
            {
                return function();
            }
            catch (EmptyResultException ex)
            {
                return ServiceResponse.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ServiceResponse.Critical(ex);
            }
        }
    }
}
