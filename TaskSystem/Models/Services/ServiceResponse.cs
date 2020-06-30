﻿using System;
using System.Runtime.Serialization;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Класс ответа на запрос
    /// </summary>
    [DataContract]
    public class ServiceResponse
    {
        /// <summary>
        /// Статус выполнения запроса
        /// </summary>
        [DataMember(Name = "status")]
        public ServiceStatus Status { get; protected set; }

        /// <summary>
        /// Сообщение при статусе "Предупреждение" или "Критическая ошибка"
        /// </summary>
        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// Метод создания объекта ответа со статусом "успешно"
        /// </summary>
        public static ServiceResponse Success()
        {
            return new ServiceResponse
            {
                Status = ServiceStatus.Success,
                ErrorMessage = string.Empty
            };
        }

        /// <summary>
        /// Метод создания объекта ответа со статусом "критическая ошибка"
        /// </summary>
        public static ServiceResponse Fail(Exception ex)
        {
            return new ServiceResponse
            {
                Status = ServiceStatus.Fail,
                ErrorMessage = ex.Message
            };
        }

        /// <summary>
        /// Метод создания объекта ответа со статусом "предупреждение"
        /// </summary>
        public static ServiceResponse Warning(string message)
        {
            return new ServiceResponse
            {
                Status = ServiceStatus.Warning,
                ErrorMessage = message
            };
        }
    }
    /// <summary>
    /// Класс ответа выполнения запроса
    /// </summary>
    /// <typeparam name="T">Тип значения результата</typeparam>
    [DataContract]
    public class ServiceResponse<T> : ServiceResponse
    {
        /// <summary>
        /// Результат выполнения запроса
        /// </summary>
        [DataMember(Name = "result")]
        public T Result { get; set; }

        /// <summary>
        /// Метод создания объекта ответа со статусом "успешно"
        /// </summary>
        public static ServiceResponse<T> Success(T value)
        {
            return new ServiceResponse<T>
            {
                Status = ServiceStatus.Success,
                Result = value
            };
        }

        /// <summary>
        /// Метод создания объекта ответа со статусом "критическая ошибка"
        /// </summary>
        public static new ServiceResponse<T> Fail(Exception ex)
        {
            return new ServiceResponse<T>
            {
                Status = ServiceStatus.Fail,
                ErrorMessage = ex.Message
            };
        }

        /// <summary>
        /// Метод создания объекта ответа со статусом "предупреждение"
        /// </summary>
        public static new ServiceResponse<T> Warning(string message)
        {
            return new ServiceResponse<T>
            {
                Status = ServiceStatus.Warning,
                ErrorMessage = message
            };
        }
    }
}
