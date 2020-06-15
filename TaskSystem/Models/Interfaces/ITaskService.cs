using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskService
    {
        ServiceResponseGeneric<IEnumerable<WorkTask>> GetLastVersions();
        ServiceResponseGeneric<IEnumerable<WorkTask>> GetTasksByStatus(WorkTaskStatus status);
        ServiceResponseGeneric<IEnumerable<WorkTask>> GetTaskByID(int taskId);
        ServiceResponse AddNewTask(WorkTask task);
        ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId, int performerID);

    }
}
