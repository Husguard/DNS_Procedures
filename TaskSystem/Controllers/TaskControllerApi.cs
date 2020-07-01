using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models.Dto;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Controllers
{
    [ApiController]
    public class TaskControllerApi : ControllerBase
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
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetLastVersions() => _taskService.GetLastVersions();

        /// <summary>
        /// Получение истории версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet("GetTaskByID")]
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTaskByID(int taskId) => _taskService.GetTaskByID(taskId);

        /// <summary>
        /// Получение последних версий всех заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId">Статус задания</param>
        [HttpGet("GetTasksByStatus")]
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId) => _taskService.GetTasksByStatus(statusId); 

        /// <summary>
        /// Получение последней версии определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpGet("GetLastVersionOfTask")]
        public ServiceResponse<WorkTaskDto> GetLastVersionOfTask(int taskId) => _taskService.GetLastVersionOfTask(taskId);

        /// <summary>
        /// Получение списка заданий, у которых определенный исполнитель
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        [HttpGet("GetTasksByPerformer")]
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId) => _taskService.GetTasksByPerformer(performerId);

        /// <summary>
        /// Получение списка заданий, у которых определенный создатель
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        [HttpGet("GetTasksByCreator")]
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId) => _taskService.GetTasksByCreator(creatorId);

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="workTask">Данные нового задания</param>
        [HttpPost("AddNewTask")]
        public ServiceResponse AddNewTask([FromBody] AddNewTaskDto workTask) => _taskService.AddNewTask(workTask);

        /// <summary>
        /// Добавление новой версии определенного задания
        /// </summary>
        /// <param name="moneyAward">Новая денежная награда</param>
        /// <param name="statusId">Новый статус задания</param>
        /// <param name="taskId">Идентификатор задания</param>
        [HttpPost("AddTaskVersion")]
        public ServiceResponse AddTaskVersion(AddTaskVersionDto version) => _taskService.AddTaskVersion(version);

        /// <summary>
        /// Получение списка заданий, у которых текущий пользователь является исполнителем
        /// </summary>
    //    [HttpGet("GetMyTasks")]
  //      public ServiceResponse<IEnumerable<WorkTaskDto>> GetMyTasks(int performerId) => _taskService.GetMyTasks();
    }
}
