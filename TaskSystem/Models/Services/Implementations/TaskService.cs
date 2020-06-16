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
        private const string EmployeeNotCreator = "Только создатель задания может отменить задание";
        private const string EmployeeNotPerformer = "Только исполнитель задания может приостанавливать, отменять и выполнять задание";
        private const string MoneyValueNegative = "Денежная награда должна быть больше нуля";

        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger)
            : base(taskRepository, employeeRepository, logger)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetLastVersions()
        {
            return ExecuteWithCatch(() =>
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
            return ExecuteWithCatch(() =>
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
        /// <param name="performerId">Идентификатор исполнителя</param>
        public ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId, int performerId)
        {
            // проверка корректности вводимых значений и существования задания
            // проверка доступности действий с заданием(отмененное не взять в работу и тп)
            return ExecuteWithCatch(() =>
            {
                if (MoneyIsNegative(moneyAward))
                    return ServiceResponse.Warning(MoneyValueNegative);
                if(statusId == WorkTaskStatus.InWork)

                if(statusId == WorkTaskStatus.Canceled)
                    if(EmployeeIsNotCreator(taskId, performerId))
                        return ServiceResponse.Warning(EmployeeNotCreator);
                _taskRepository.AddTaskVersion(moneyAward, statusId, taskId, performerId);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Получение всех версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetTaskByID(int taskId)
        {
            return ExecuteWithCatch(() =>
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
            return ExecuteWithCatch(() =>
            {
                var taskVersion = _taskRepository.GetLastVersionOfTask(taskId);
                return ServiceResponseGeneric<WorkTask>.Success(taskVersion);
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
        private bool EmployeeIsNotCreator(int taskId, int employeeId)
        {
            if (employeeId != _taskRepository.GetLastVersionOfTask(taskId).CreatorId)
            {
                _logger.LogWarning("Employee №{0} is not creator of №{1} task", employeeId, taskId);
                return true;
            }
            return false;
        }
        private bool EmployeeIsNotPerformer(int taskId, int employeeId)
        {
            var task = _taskRepository.GetLastVersionOfTask(taskId);
            return (employeeId == task.PerformerId);
        }
    }
}
