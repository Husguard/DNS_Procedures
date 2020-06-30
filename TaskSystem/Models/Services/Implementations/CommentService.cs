using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с комментариями к заданиям
    /// </summary>
    public class CommentService : BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        protected readonly ITaskRepository _taskRepository;
        protected readonly IEmployeeRepository _employeeRepository;

        private const string CommentTooLong = "Комментарий слишком длинный, ограничение в 300 символов";
        private const string EmptyComment = "Комментарий не должен быть пустым";

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="commentRepository">Репозиторий комментариев</param>
        /// <param name="taskRepository">Репозиторий заданий</param>
        /// <param name="employeeRepository">Репозиторий работников</param>
        /// <param name="logger">Инициализатор логгера</param>
        public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger, UserManager manager)
            : base(logger, manager)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
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
        /// <param name="commentDto">Данные комментария</param>
        public ServiceResponse AddCommentToTask(int taskId, string message)
        {
            return ExecuteWithCatch(() =>
            {
                if (CommentIsEmpty(message))
                    return ServiceResponse.Warning(EmptyComment);

                if (CommentIsTooLong(message))
                    return ServiceResponse.Warning(CommentTooLong);

                if (TaskIsNotExists(taskId))
                    return ServiceResponse.Warning(WorkTaskNotFound);

                if (EmployeeIsNotExists(_manager._currentUserId))
                    return ServiceResponse.Warning(EmployeeNotFound);

                _commentRepository.AddCommentToTask(message, taskId, _manager._currentUserId);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Метод проверки строки на пустой комментарий
        /// </summary>
        /// <param name="message"></param>
        private bool CommentIsEmpty(string message)
        {
            if(string.IsNullOrWhiteSpace(message))
            {
                _logger.LogWarning("Попытка добавить пустой комментарий");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки длины введенного комментария
        /// </summary>
        /// <param name="message">Комментарий</param>
        private bool CommentIsTooLong(string message)
        {
            if(message.Length > 100)
            {
                _logger.LogWarning("Попытка добавить комментарий с длиной {0}", message.Length);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки существования задания с введенным идентификатором
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        private bool TaskIsNotExists(int taskId)
        {
            if (_taskRepository.GetTaskByID(taskId) == null)
            {
                _logger.LogWarning("Задания с Id = {0} не существует", taskId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки существования работника с введеным идентификатором
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        private bool EmployeeIsNotExists(int employeeId)
        {
            if (_employeeRepository.GetEmployeeById(employeeId) == null)
            {
                _logger.LogWarning("Работника с Id = {0} не существует", employeeId);
                return true;
            }
            return false;
        }
    }
}
