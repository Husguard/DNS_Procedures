using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Services
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger _logger;

        public TaskService(ITaskRepository taskRepository, ILogger logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetLastVersions()
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var workTasks = _taskRepository.GetLastVersions();
                return ServiceResponseGeneric<IEnumerable<WorkTask>>.Success(workTasks);
            });
        }
        public ServiceResponseGeneric<IEnumerable<WorkTask>> GetTasksByStatus(WorkTaskStatus statusId)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var workTasks = _taskRepository.GetTasksByStatus(statusId);
                return ServiceResponseGeneric<IEnumerable<WorkTask>>.Success(workTasks);
            });
        }
        public ServiceResponse AddNewTask(WorkTask task)
        {
            return ExecuteWithCatch(() =>
            {
                _taskRepository.AddNewTask(task);
                return ServiceResponse.Success();
            });
        }
        public ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId, int performerID)
        {
            return ExecuteWithCatch(() =>
            {
                _taskRepository.AddTaskVersion(moneyAward, statusId, taskId, performerID);
                return ServiceResponse.Success();
            });
        }
    }
}
