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

        /// <summary>
        /// Получение последних версий всех заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId">Статус задания</param>
        [HttpGet]
        [Route("GetTasksByStatus")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId) => _taskService.GetTasksByStatus(statusId); 

        /// <summary>
        /// Получение последней версии определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet]
        [Route("GetLastVersionOfTask")]
        public ServiceResponseGeneric<WorkTaskDto> GetLastVersionOfTask(int taskId) => _taskService.GetLastVersionOfTask(taskId);

        /// <summary>
        /// Получение списка заданий, у которых определенный исполнитель
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        [HttpGet]
        [Route("GetTasksByPerformer")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId) => _taskService.GetTasksByPerformer(performerId);

        /// <summary>
        /// Получение списка заданий, у которых определенный создатель
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        [HttpGet]
        [Route("GetTasksByCreator")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId) => _taskService.GetTasksByCreator(creatorId);

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="workTask">Данные нового задания</param>
        [HttpPost]
        [Route("AddNewTask")]
        public ServiceResponse AddNewTask(WorkTaskDto workTask) => _taskService.AddNewTask(workTask);

        /// <summary>
        /// Добавление новой версии определенного задания
        /// </summary>
        /// <param name="moneyAward">Новая денежная награда</param>
        /// <param name="statusId">Новый статус задания</param>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpPost]
        [Route("AddTaskVersion")]
        public ServiceResponse AddTaskVersion(decimal moneyAward, WorkTaskStatus statusId, int taskId) => _taskService.AddTaskVersion(moneyAward, statusId, taskId);

    }
}
