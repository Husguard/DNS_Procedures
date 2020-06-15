using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Services
{
    public class BaseService
    {
        /// <summary>
        /// Выполняет функцию, обворачивая ее в try-catch
        /// </summary>
        /// <param name="function">Функция</param>
        protected ServiceResponseGeneric<T> ExecuteWithCatchGeneric<T>(Func<ServiceResponseGeneric<T>> function)
        {
            try
            {
                return function();
            }
            catch (SqlException ex)
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
        }
    }
}
