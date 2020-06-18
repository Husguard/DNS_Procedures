using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с заданиями
    /// </summary>
    public class TaskService : BaseService, ITaskService
    {
        private const string MoneyValueNegative = "Денежная награда должна быть больше нуля";
        private const string CanceledTask = "У отмененного задания нельзя изменить статус";
        private const string NotAllowedToChange = "Вы можете только принять задание";

        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger)
            : base(taskRepository, employeeRepository, logger)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetLastVersions()
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetLastVersions();
                return ServiceResponseGeneric<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Метод получения последних версий заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId"></param>
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId)
        {
            // поиск существования статуса
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetTasksByStatus(statusId);
                return ServiceResponseGeneric<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        public ServiceResponse AddNewTask(WorkTaskDto task)
        {
            return ExecuteWithCatch(() =>
            {
                _taskRepository.AddNewTask(new WorkTask(task));
                return ServiceResponse.Success();
            });
            
        }

        /// <summary>
        /// Добавление новой версии задания, при принятии задания меняется только 
        /// награда от нового исполнителя, при других статусах используются старые значения
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Новый статус</param>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId)
        {
            return ExecuteWithCatch(() =>
            {
                if (MoneyIsNegative(moneyAward))
                    return ServiceResponse.Warning(MoneyValueNegative);

                var task = _taskRepository.GetLastVersionOfTask(taskId);
                if (task == null)
                    return ServiceResponse.Warning(WorkTaskNotFound);

                if (task.Status == WorkTaskStatus.Canceled)
                    return ServiceResponse.Warning(CanceledTask);

                if (task.CreatorId != _currentUser || task.PerformerId != _currentUser || task.PerformerId != null)
                {
                    _logger.LogWarning("");
                    return ServiceResponse.Warning(NotAllowedToChange);
                }
  
                _taskRepository.AddTaskVersion(moneyAward, statusId, taskId, _currentUser);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Получение всех версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTaskByID(int taskId)
        {
            return ExecuteWithCatch(() =>
            {
                var taskVersions = _taskRepository.GetTaskByID(taskId);
                return ServiceResponseGeneric<IEnumerable<WorkTaskDto>>.Success(
                    taskVersions.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<WorkTaskDto> GetLastVersionOfTask(int taskId)
        {
            return ExecuteWithCatch(() =>
            {
                var taskVersion = _taskRepository.GetLastVersionOfTask(taskId);
                return ServiceResponseGeneric<WorkTaskDto>.Success(new WorkTaskDto(taskVersion));
            });
        }

        private bool MoneyIsNegative(decimal money)
        {
            if (money < 0)
            {
                _logger.LogWarning("Money = {0} is negative", money);
                return true;
            }
            return false;
        }
    }
}
