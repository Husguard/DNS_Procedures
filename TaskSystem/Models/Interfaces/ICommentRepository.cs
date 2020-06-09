using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Получение комментариев с задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        /// <returns>IEnumerable<Comment></returns>
        public IEnumerable<Comment> GetCommentsOfTask(int taskId);
        /// <summary>
        /// Получение комментариев работника со всех заданий
        /// </summary>
        /// <param name="employee">Объект работника</param>
        /// <returns>IEnumerable<Comment></returns>
        public IEnumerable<Comment> GetCommentsOfEmployee(int employeeId);
        /// <summary>
        /// Добавление комментария к заданию от работника
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="task">Объект задания</param>
        /// <param name="employee">Объект работника</param>
        public void AddCommentToTask(string message, int taskId, int employeeId);
    }
}
