using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Dto;
using TaskSystem.Models.Repositories.Interfaces;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Models.Services.Implementations
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с комментариями к заданиям
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly BaseService _baseService;

        private const string WorkTaskNotFound = "Задание не было найдено";
        private const string EmployeeNotFound = "Работник не найден";

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="commentRepository">Репозиторий комментариев</param>
        /// <param name="taskRepository">Репозиторий заданий</param>
        /// <param name="employeeRepository">Репозиторий работников</param>
        public CommentService(ICommentRepository commentRepository, ITaskRepository taskRepository, IEmployeeRepository employeeRepository, BaseService baseService)
        {
            _baseService = baseService;
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получение всех комментариев задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponse<IEnumerable<CommentDto>> GetCommentsOfTask(int taskId)
        {
            return _baseService.ExecuteWithCatch(() =>
            {
                if (TaskIsNotExists(taskId))
                    return ServiceResponse<IEnumerable<CommentDto>>.Warning("Задание не было найдено");

                var taskComments = _commentRepository.GetCommentsOfTask(taskId);
                return ServiceResponse<IEnumerable<CommentDto>>.Success(
                    taskComments.Select((comment) => new CommentDto(comment)));
            });
        }

        /// <summary>
        /// Получение всех комментариев от работника
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponse<IEnumerable<CommentDto>> GetCommentsOfEmployee(int employeeId)
        {
            return _baseService.ExecuteWithCatch(() =>
            {
                if (EmployeeIsNotExists(employeeId))
                    return ServiceResponse<IEnumerable<CommentDto>>.Warning(EmployeeNotFound);

                var employeeComments = _commentRepository.GetCommentsOfEmployee(employeeId);
                return ServiceResponse<IEnumerable<CommentDto>>.Success(
                    employeeComments.Select((comment) => new CommentDto(comment)));
            });
        }

        /// <summary>
        /// Добавление комментария к заданию от работника
        /// </summary>
        /// <param name="commentDto">Данные комментария</param>
        public ServiceResponse AddCommentToTask(int taskId, string message)
        {
            return _baseService.ExecuteWithCatch(() =>
            {
                if (CommentIsEmpty(message))
                    return ServiceResponse.Warning("Комментарий не должен быть пустым");

                if (CommentIsTooLong(message))
                    return ServiceResponse.Warning("Комментарий слишком длинный, ограничение в 300 символов");

                if (TaskIsNotExists(taskId))
                    return ServiceResponse.Warning(WorkTaskNotFound);

                if (EmployeeIsNotExists(_baseService.Manager.CurrentUserId))
                    return ServiceResponse.Warning(EmployeeNotFound);

                _commentRepository.AddCommentToTask(message, taskId, _baseService.Manager.CurrentUserId);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Метод проверки строки на пустой комментарий
        /// </summary>
        /// <param name="message"></param>
        private bool CommentIsEmpty(string message)
        {
            return (string.IsNullOrWhiteSpace(message));
        }

        /// <summary>
        /// Метод проверки длины введенного комментария
        /// </summary>
        /// <param name="message">Комментарий</param>
        private bool CommentIsTooLong(string message)
        {
            return (message.Length > 300);
        }

        /// <summary>
        /// Метод проверки существования задания с введенным идентификатором
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        private bool TaskIsNotExists(int taskId)
        {
            return (!_taskRepository.GetTaskByID(taskId).Any());
        }

        /// <summary>
        /// Метод проверки существования работника с введеным идентификатором
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        private bool EmployeeIsNotExists(int employeeId)
        {
            return (_employeeRepository.GetEmployeeById(employeeId) == null);
        }
    }
}
