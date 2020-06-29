using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Dto;
using TaskSystem.Models.Dto;
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
        private const string CanceledOrCompletedTask = "У отмененного или выполненного задания нельзя изменить статус";
        private const string NotAllowedToChange = "Вам нельзя изменять статус задания";
        private const string OnlyAccept = "Вы можете только принять задание";

        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="taskRepository">Репозиторий заданий</param>
        /// <param name="employeeRepository">Репозиторий работников</param>
        /// <param name="logger">Инициализатор логгера</param>
        public TaskService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger, UserManager manager)
            : base(logger, manager)
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
        /// Получение заданий, у которых определенный создатель
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId)
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetTasksByCreator(creatorId);
                return ServiceResponseGeneric<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Получение заданий, у которых определенный исполнитель
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId)
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetTasksByPerformer(performerId);
                return ServiceResponseGeneric<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }


        /// <summary>
        /// Метод получения последних версий заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId">Статус задания</param>
        public ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId)
        {
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
        public ServiceResponse AddNewTask(AddNewTaskDto task)
        {
            task.CreatorId = _manager._currentUserId;
            return ExecuteWithCatch(() =>
            {
                _taskRepository.AddNewTask(task.Name, task.Description, task.ThemeId, task.CreatorId, task.ExpireDate);
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
        public ServiceResponse AddTaskVersion(AddTaskVersionDto versionDto)
        {
            return ExecuteWithCatch(() =>
            {
                if (MoneyIsNegative(versionDto.MoneyAward))
                    return ServiceResponse.Warning(MoneyValueNegative);

                var task = _taskRepository.GetLastVersionOfTask(versionDto.TaskId);
                if (task == null)
                    return ServiceResponse.Warning(WorkTaskNotFound);

                if (task.Status == WorkTaskStatus.Canceled || task.Status == WorkTaskStatus.Completed)
                    return ServiceResponse.Warning(CanceledOrCompletedTask);

                if ((task.Creator.Id != _manager._currentUserId))
                {
                    if ((task.Performer == null))
                    {
                        if (versionDto.Status != WorkTaskStatus.InWork)
                        {
                            _logger.LogWarning("Пользователь №{0} может только принять задание №{1}", _manager._currentUserId, versionDto.TaskId);
                            return ServiceResponse.Warning(OnlyAccept);
                        }
                    }
                    else if (task.Performer.Id != _manager._currentUserId)
                    {
                        _logger.LogWarning("Пользователю №{0} нельзя изменять задание №{1}", _manager._currentUserId, versionDto.TaskId);
                        return ServiceResponse.Warning(NotAllowedToChange);
                    }
                }

                _taskRepository.AddTaskVersion(versionDto.MoneyAward, versionDto.Status, versionDto.TaskId, _manager._currentUserId);
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

        /// <summary>
        /// Метод проверки на отрицательное число введенных денег
        /// </summary>
        /// <param name="money">Сумма денег</param>
        private bool MoneyIsNegative(decimal? money)
        {
            if (money.HasValue)
            {
                if (money.Value < 0)
                {
                    _logger.LogWarning("Значение денег {0} отрицательно", money);
                    return true;
                }
            }
            return false;
        }
    }
}
