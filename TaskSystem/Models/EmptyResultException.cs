using System;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Исключение, возникающее при возвращении из БД пустого результата
    /// </summary>
    [Serializable]
    internal class EmptyResultException : Exception
    {
        /// <summary>
        /// Конструктор исключения без сообщения
        /// </summary>
        public EmptyResultException() : base("Объект не был найден") { }

        /// <summary>
        /// Конструктор исключения с сообщением
        /// </summary>
        /// <param name="message">Сообщение</param>
        public EmptyResultException(string message) : base(message) { }
    }
}