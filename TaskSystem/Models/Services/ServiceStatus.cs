namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Перечисление статусов выполнения запроса
    /// </summary>
    public enum ServiceStatus : byte
    {
        /// <summary>
        /// Статус "Успешно"
        /// </summary>
        Success = 0,
        /// <summary>
        /// Статус "Предупреждение"
        /// </summary>
        Warning = 1,
        /// <summary>
        /// Статус "Критическая ошибка"
        /// </summary>
        Fail = 2
    }
}
