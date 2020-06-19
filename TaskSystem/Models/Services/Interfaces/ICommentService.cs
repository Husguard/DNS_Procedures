using System.Collections.Generic;
using TaskSystem.Dto;
using TaskSystem.Models.Services;

namespace TaskSystem.Models.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса взаимодействия с комментариями
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Получение всех комментариев задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponseGeneric<IEnumerable<CommentDto>> GetCommentsOfTask(int taskId);

        /// <summary>
        /// Получение всех комментариев работника
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        ServiceResponseGeneric<IEnumerable<CommentDto>> GetCommentsOfEmployee(int employeeId);

        /// <summary>
        /// Добавление комментария к заданию от текущего пользователя
        /// </summary>
        /// <param name="commentDto">Данные комментария</param>
        ServiceResponse AddCommentToTask(CommentDto commentDto);
    }
}
