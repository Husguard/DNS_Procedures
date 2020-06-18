using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Services;

namespace TaskSystem.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="commentService">Сервис комментариев</param>
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Получение всех работников
        /// </summary>
        [HttpGet]
        [Route("GetCommentsOfTask")]
        public ServiceResponseGeneric<IEnumerable<CommentDto>> GetCommentsOfTask(int taskId) => _commentService.GetCommentsOfTask(taskId);

        [HttpGet]
        [Route("GetCommentsOfEmployee")]
        public ServiceResponseGeneric<IEnumerable<CommentDto>> GetCommentsOfEmployee(int employeeId) => _commentService.GetCommentsOfEmployee(employeeId);

        [HttpPost]
        [Route("AddCommentToTask")]
        public ServiceResponse AddCommentToTask(CommentDto commentDto) => _commentService.AddCommentToTask(commentDto);
    }
}
