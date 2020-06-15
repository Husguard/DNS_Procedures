using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Класс ответа выполнения запроса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponseGeneric<T> : ServiceResponse
    {
        public T Result { get; set; }
        public static ServiceResponseGeneric<T> Success(T value)
        {
            return new ServiceResponseGeneric<T>
            {
                Status = ServiceStatus.Success,
                Result = value
            };
        }
        public static new ServiceResponseGeneric<T> Fail(Exception ex)
        {
            return new ServiceResponseGeneric<T>
            {
                Status = ServiceStatus.Fail,
                ErrorMessage = ex.Message
            };
        }
        public static new ServiceResponseGeneric<T> Warning(string message)
        {
            return new ServiceResponseGeneric<T>
            {
                Status = ServiceStatus.Warning,
                ErrorMessage = message
            };
        }
    }
}
