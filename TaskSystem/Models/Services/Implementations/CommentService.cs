using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly ILogger _logger;

        public CommentService(ICommentRepository commentRepository, ILogger logger)
        {
            _commentRepository = commentRepository;
            _logger = logger;
        }

        /// <summary>
        /// Получение всех комментариев задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<IEnumerable<Comment>> GetCommentsOfTask(int taskId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var taskComments = _commentRepository.GetCommentsOfTask(taskId);
                return ServiceResponseGeneric<IEnumerable<Comment>>.Success(taskComments);
            });
        }

        /// <summary>
        /// Получение всех комментариев от работника
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponseGeneric<IEnumerable<Comment>> GetCommentsOfEmployee(int employeeId)
        {
            // проверка существования работника
            return ExecuteWithCatchGeneric(() =>
            {
                var employeeComments = _commentRepository.GetCommentsOfEmployee(employeeId);
                return ServiceResponseGeneric<IEnumerable<Comment>>.Success(employeeComments);
            });
        }

        /// <summary>
        /// Добавление комментария к заданию от работника
        /// </summary>
        /// <param name="message">Комментарий</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="employeeId">Идентификатор работника</param>
        public ServiceResponse AddCommentToTask(string message, int taskId, int employeeId)
        {
            // проверка строки, существования задания, существования работника
            return ExecuteWithCatch(() =>
            {
                _commentRepository.AddCommentToTask(message, taskId, employeeId);
                return ServiceResponse.Success();
            });
        }
    }
}
