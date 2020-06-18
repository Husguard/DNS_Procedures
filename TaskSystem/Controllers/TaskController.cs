using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services;

namespace TaskSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="taskService">Сервис работников</param>
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        [HttpGet]
        [Route("GetLastVersions")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetLastVersions() => _taskService.GetLastVersions();

        /// <summary>
        /// Получение истории версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet]
        [Route("GetTaskByID")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTaskByID(int taskId) => _taskService.GetTaskByID(taskId);

        [HttpGet]
        [Route("GetTasksByStatus")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId) => _taskService.GetTasksByStatus(statusId); // проблема в работе процедуры
        
        [HttpGet]
        [Route("GetLastVersionOfTask")]
        public ServiceResponseGeneric<WorkTaskDto> GetLastVersionOfTask(int taskId) => _taskService.GetLastVersionOfTask(taskId); // проблема в работе процедуры


        [HttpGet]
        [Route("GetTasksByPerformer")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId) => _taskService.GetTasksByPerformer(performerId);

        [HttpGet]
        [Route("GetTasksByCreator")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId) => _taskService.GetTasksByCreator(creatorId);

        [HttpPost]
        [Route("AddNewTask")]
        public ServiceResponse AddNewTask(WorkTaskDto workTask) => _taskService.AddNewTask(workTask);

        [HttpPost]
        [Route("AddTaskVersion")]
        public ServiceResponse AddTaskVersion(decimal moneyAward, WorkTaskStatus statusId, int taskId) => _taskService.AddTaskVersion(moneyAward, statusId, taskId);  // проблема в работе процедуры


    }
}
