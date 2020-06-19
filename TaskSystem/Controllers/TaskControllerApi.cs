using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services;

namespace TaskSystem.Controllers
{
    public class TaskControllerApi : Controller
    {
        private readonly ITaskService _taskService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="taskService">Сервис работников</param>
        public TaskControllerApi(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        [HttpGet("GetLastVersions")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetLastVersions() => _taskService.GetLastVersions();

        /// <summary>
        /// Получение истории версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet("GetTaskByID")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTaskByID(int taskId) => _taskService.GetTaskByID(taskId);

        /// <summary>
        /// Получение последних версий всех заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId">Статус задания</param>
        [HttpGet("GetTasksByStatus")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId) => _taskService.GetTasksByStatus(statusId); 

        /// <summary>
        /// Получение последней версии определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet("GetLastVersionOfTask")]
        public ServiceResponseGeneric<WorkTaskDto> GetLastVersionOfTask(int taskId) => _taskService.GetLastVersionOfTask(taskId);

        /// <summary>
        /// Получение списка заданий, у которых определенный исполнитель
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        [HttpGet("GetTasksByPerformer")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId) => _taskService.GetTasksByPerformer(performerId);

        /// <summary>
        /// Получение списка заданий, у которых определенный создатель
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        [HttpGet("GetTasksByCreator")]
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId) => _taskService.GetTasksByCreator(creatorId);

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="workTask">Данные нового задания</param>
        [HttpPost("AddNewTask")]
        public ServiceResponse AddNewTask(WorkTaskDto workTask) => _taskService.AddNewTask(workTask);

        /// <summary>
        /// Добавление новой версии определенного задания
        /// </summary>
        /// <param name="moneyAward">Новая денежная награда</param>
        /// <param name="statusId">Новый статус задания</param>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpPost("AddTaskVersion")]
        public ServiceResponse AddTaskVersion(decimal moneyAward, WorkTaskStatus statusId, int taskId) => _taskService.AddTaskVersion(moneyAward, statusId, taskId);

    }
}
