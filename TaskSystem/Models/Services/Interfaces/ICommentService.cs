﻿using System.Collections.Generic;
using TaskSystem.Models.Dto;

namespace TaskSystem.Models.Services.Interfaces
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
        ServiceResponse<IEnumerable<CommentDto>> GetCommentsOfTask(int taskId);

        /// <summary>
        /// Получение всех комментариев работника
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        ServiceResponse<IEnumerable<CommentDto>> GetCommentsOfEmployee(int employeeId);

        /// <summary>
        /// Добавление комментария к заданию от текущего пользователя
        /// </summary>
        /// <param name="commentDto">Данные комментария</param>
        ServiceResponse AddCommentToTask(int taskId, string message);
    }
}
