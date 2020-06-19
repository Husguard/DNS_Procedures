using System;
using System.Runtime.Serialization;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Класс ответа выполнения запроса
    /// </summary>
    /// <typeparam name="T">Тип значения результата</typeparam>
    [DataContract]
    public class ServiceResponseGeneric<T> : ServiceResponse
    {
        /// <summary>
        /// Результат выполнения запроса
        /// </summary>
        [DataMember(Name = "result")]
        public T Result { get; set; }

        /// <summary>
        /// Метод создания объекта ответа со статусом "успешно"
        /// </summary>
        public static ServiceResponseGeneric<T> Success(T value)
        {
            return new ServiceResponseGeneric<T>
            {
                Status = ServiceStatus.Success,
                Result = value
            };
        }

        /// <summary>
        /// Метод создания объекта ответа со статусом "критическая ошибка"
        /// </summary>
        public static new ServiceResponseGeneric<T> Fail(Exception ex)
        {
            return new ServiceResponseGeneric<T>
            {
                Status = ServiceStatus.Fail,
                ErrorMessage = ex.Message
            };
        }

        /// <summary>
        /// Метод создания объекта ответа со статусом "предупреждение"
        /// </summary>
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
