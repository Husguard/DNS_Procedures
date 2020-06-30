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
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="taskRepository">Репозиторий заданий</param>
        /// <param name="employeeRepository">Репозиторий работников</param>
        /// <param name="logger">Инициализатор логгера</param>
        public TaskService(ITaskRepository taskRepository, ILoggerFactory logger, UserManager manager)
            : base(logger, manager)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetLastVersions()
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetLastVersions();
                return ServiceResponse<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }


        /// <summary>
        /// Получение заданий, у которых определенный создатель
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId)
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetTasksByCreator(creatorId);
                return ServiceResponse<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Получение заданий, у которых определенный исполнитель
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId)
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetTasksByPerformer(performerId);
                return ServiceResponse<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }


        /// <summary>
        /// Метод получения последних версий заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId">Статус задания</param>
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus statusId)
        {
            return ExecuteWithCatch(() =>
            {
                var workTasks = _taskRepository.GetTasksByStatus(statusId);
                return ServiceResponse<IEnumerable<WorkTaskDto>>.Success(
                    workTasks.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        public ServiceResponse AddNewTask(AddNewTaskDto task)
        {
            return ExecuteWithCatch(() =>
            {
                if (IsThemeChoosed(task.ThemeId))
                    return ServiceResponse.Warning("Выберите тему");

                if (IsEmptyNameOrDescription(task))
                    return ServiceResponse.Warning("Заполните название и/или описание темы");

                if (IsTaskNameTooLong(task.Name))
                    return ServiceResponse.Warning("Название задания слишком длинное, ограничение в 100 символов");

                if (IsTaskDescriptionTooLong(task.Description))
                    return ServiceResponse.Warning("Описание задания слишком длинное, ограничение в 300 символов");

                _taskRepository.AddNewTask(task.Name, task.Description, task.ThemeId, _manager.CurrentUserId, task.ExpireDate);
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
                if (IsMoneyNegative(versionDto.MoneyAward))
                    return ServiceResponse.Warning("Денежная награда должна быть больше нуля");

                var task = _taskRepository.GetLastVersionOfTask(versionDto.TaskId);

                if (task == null)
                    return ServiceResponse.Warning("Задание не было найдено");

                if (task.Status == WorkTaskStatus.Canceled || task.Status == WorkTaskStatus.Completed)
                    return ServiceResponse.Warning("У отмененного или выполненного задания нельзя изменить статус");

                if ((task.Creator.Id != _manager.CurrentUserId))
                {
                    if ((task.Performer == null))
                    {
                        if (versionDto.Status != WorkTaskStatus.InWork)
                        {
                            _logger.LogWarning("Пользователь №{0} может только принять задание №{1}", _manager.CurrentUserId, versionDto.TaskId);
                            return ServiceResponse.Warning("Вы можете только принять задание");
                        }
                    }
                    else if (task.Performer.Id != _manager.CurrentUserId)
                    {
                        _logger.LogWarning("Пользователю №{0} нельзя изменять задание №{1}", _manager.CurrentUserId, versionDto.TaskId);
                        return ServiceResponse.Warning("Вам нельзя изменять статус задания");
                    }
                }

                if (versionDto.Status != WorkTaskStatus.InWork)
                    if (task.Status == versionDto.Status)
                        return ServiceResponse.Warning("Нельзя ставить тот же самый статус");

                _taskRepository.AddTaskVersion(versionDto.MoneyAward, versionDto.Status, versionDto.TaskId, _manager.CurrentUserId);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Получение всех версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponse<IEnumerable<WorkTaskDto>> GetTaskByID(int taskId)
        {
            return ExecuteWithCatch(() =>
            {
                var taskVersions = _taskRepository.GetTaskByID(taskId);

                return ServiceResponse<IEnumerable<WorkTaskDto>>.Success(
                    taskVersions.Select((task) => new WorkTaskDto(task)));
            });
        }

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponse<WorkTaskDto> GetLastVersionOfTask(int taskId)
        {
            return ExecuteWithCatch(() =>
            {
                var taskVersion = _taskRepository.GetLastVersionOfTask(taskId);
                return ServiceResponse<WorkTaskDto>.Success(new WorkTaskDto(taskVersion));
            });
        }

        /// <summary>
        /// Метод проверки на отрицательное число введенных денег
        /// </summary>
        /// <param name="money">Сумма денег</param>
        private bool IsMoneyNegative(decimal money)
        {
            return (money < 0);
        }

        private bool IsTaskDescriptionTooLong(string description)
        {
            return (description.Length > 300);
        }

        private bool IsTaskNameTooLong(string name)
        {
            return (name.Length > 100);
        }

        private bool IsEmptyNameOrDescription(AddNewTaskDto task)
        {
            return (string.IsNullOrWhiteSpace(task.Name) || string.IsNullOrWhiteSpace(task.Description));
        }

        private bool IsThemeChoosed(int themeId)
        {
            return (themeId == 0);
        }
    }
}
