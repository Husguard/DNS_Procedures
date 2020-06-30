using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Services;

namespace TaskSystem.Controllers
{
    [ApiController]
    public class CommentControllerApi : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="commentService">Сервис комментариев</param>
        public CommentControllerApi(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Получение всех комментариев определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet("GetCommentsOfTask")]
        public ServiceResponse<IEnumerable<CommentDto>> GetCommentsOfTask(int taskId) => _commentService.GetCommentsOfTask(taskId);

        /// <summary>
        /// Получение всех комментариев определенного работника
        /// </summary>
        /// <param name="employeeId">Идентификатор работника</param>
        [HttpGet("GetCommentsOfEmployee")]
        public ServiceResponse<IEnumerable<CommentDto>> GetCommentsOfEmployee(int employeeId) => _commentService.GetCommentsOfEmployee(employeeId);

        /// <summary>
        /// Добавление комментария к заданию
        /// </summary>
        /// <param name="commentDto">Данные комментария</param>
        [HttpPost("AddCommentToTask")]
        public ServiceResponse AddCommentToTask(CommentDto commentDto)
        {
            return _commentService.AddCommentToTask(commentDto.TaskId, commentDto.Message);
        }
    }
}
