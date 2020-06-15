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
    /// Сервис проверки и выполнения запросов, связанных с заданиями
    /// </summary>
    public class TaskService : BaseService, ITaskService
    {
        /// <summary>
        /// Репозиторий заданий и логгер
        /// </summary>
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger _logger;

        public TaskService(ITaskRepository taskRepository, ILogger logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetLastVersions()
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var workTasks = _taskRepository.GetLastVersions();
                return ServiceResponseGeneric<IEnumerable<WorkTask>>.Success(workTasks);
            });
        }

        /// <summary>
        /// Метод получения последних версий заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId"></param>
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetTasksByStatus(WorkTaskStatus statusId)
        {
            // поиск существования статуса
            return ExecuteWithCatchGeneric(() =>
            {
                var workTasks = _taskRepository.GetTasksByStatus(statusId);
                return ServiceResponseGeneric<IEnumerable<WorkTask>>.Success(workTasks);
            });
        }

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        public ServiceResponse AddNewTask(WorkTask task)
        {
            // проверка корректности вводимых значений в задании
            return ExecuteWithCatch(() =>
            {
                _taskRepository.AddNewTask(task);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Добавление новой версии задания, при принятии задания меняется только 
        /// награда от нового исполнителя, при других статусах используются старые значения
        /// ввиду этого нужна перегрузка метода
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Новый статус</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="performerID">Идентификатор исполнителя</param>
        public ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId, int performerID)
        {
            // проверка корректности вводимых значений и существования задания
            // проверка доступности действий с заданием(отмененное не взять в работу и тп)
            return ExecuteWithCatch(() =>
            {
                _taskRepository.AddTaskVersion(moneyAward, statusId, taskId, performerID);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Получение всех версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetTaskByID(int taskId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var taskVersions = _taskRepository.GetTaskByID(taskId);
                return ServiceResponseGeneric<IEnumerable<WorkTask>>.Success(taskVersions);
            });
        }

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<WorkTask> GetLastVersionOfTask(int taskId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var taskVersion = _taskRepository.GetLastVersionOfTask(taskId);
                return ServiceResponseGeneric<WorkTask>.Success(taskVersion);
            });
        }
    }
}
