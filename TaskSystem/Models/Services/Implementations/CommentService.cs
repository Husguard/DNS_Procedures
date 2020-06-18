using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с комментариями к заданиям
    /// </summary>
    public class CommentService : BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private const string CommentTooLong = "Комментарий слишком длинный, ограничение в 300 символов";

        public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger)
            : base(taskRepository, employeeRepository, logger)
        {
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Получение всех комментариев задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<IEnumerable<CommentDto>> GetCommentsOfTask(int taskId)
        {
            return ExecuteWithCatch(() =>
            {
                if (TaskIsNotExists(taskId))
                    return ServiceResponseGeneric<IEnumerable<CommentDto>>.Warning(WorkTaskNotFound);
                var taskComments = _commentRepository.GetCommentsOfTask(taskId);
                return ServiceResponseGeneric<IEnumerable<CommentDto>>.Success(
                    taskComments.Select((comment) => new CommentDto(comment)));
            });
        }

        /// <summary>
        /// Получение всех комментариев от работника
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponseGeneric<IEnumerable<CommentDto>> GetCommentsOfEmployee(int employeeId)
        {
            return ExecuteWithCatch(() =>
            {
                if (EmployeeIsNotExists(employeeId))
                    return ServiceResponseGeneric<IEnumerable<CommentDto>>.Warning(EmployeeNotFound);
                var employeeComments = _commentRepository.GetCommentsOfEmployee(employeeId);
                return ServiceResponseGeneric<IEnumerable<CommentDto>>.Success(
                    employeeComments.Select((comment) => new CommentDto(comment)));
            });
        }

        /// <summary>
        /// Добавление комментария к заданию от работника
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponse AddCommentToTask(CommentDto commentDto)
        {
            return ExecuteWithCatch(() =>
            {
                if (CommentIsTooLong(commentDto.Message))
                    return ServiceResponse.Warning(CommentTooLong);
                if (TaskIsNotExists(commentDto.TaskId))
                    return ServiceResponse.Warning(WorkTaskNotFound);
                if (EmployeeIsNotExists(_currentUser))
                    return ServiceResponse.Warning(EmployeeNotFound);
                _commentRepository.AddCommentToTask(commentDto.Message, commentDto.TaskId, _currentUser);
                return ServiceResponse.Success();
            });
        }
        
        /// <summary>
        /// Метод проверки длины введенного комментария
        /// </summary>
        /// <param name="message">Комментарий</param>
        private bool CommentIsTooLong(string message)
        {
            if(message.Length > 100)
            {
                _logger.LogWarning("Message with Length = {0} is incorrect", message.Length);
                return true;
            }
            return false;
        }
    }
}
