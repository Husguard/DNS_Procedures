using System.Collections.Generic;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для комментариев
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Получение комментариев с задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        IEnumerable<Comment> GetCommentsOfTask(int taskId);

        /// <summary>
        /// Получение комментариев работника со всех заданий
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        IEnumerable<Comment> GetCommentsOfEmployee(int employeeId);

        /// <summary>
        /// Добавление комментария к заданию от работника
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="employeeId">Идентификатор работника</param>
        void AddCommentToTask(string message, int taskId, int employeeId);
    }
}
