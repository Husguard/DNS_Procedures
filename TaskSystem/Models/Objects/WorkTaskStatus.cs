namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Перечисление статусов задания
    /// </summary>
    public enum WorkTaskStatus : byte
    {
        /// <summary>
        /// Статус нового задания
        /// </summary>
        New = 1,
        /// <summary>
        /// Статус задания, находящегося в работе
        /// </summary>
        InWork = 2,
        /// <summary>
        /// Статус приостановленного исполнителем задания
        /// </summary>
        Paused = 3,
        /// <summary>
        /// Статус выполненного исполнителем задания
        /// </summary>
        Completed = 4,
        /// <summary>
        /// Статус отмененного задания создателем или исполнителем
        /// </summary>
        Canceled = 5
    }
}
