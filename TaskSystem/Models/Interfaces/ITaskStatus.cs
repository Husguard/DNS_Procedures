using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskStatus
    {
        public void AcceptTaskByEmployee(Employee employee, decimal award);
        public void CancelTask();
        public void CompleteTask();
        public void PauseTask();
        public bool IsTaskExpire();
        public void UpdateStatusOfTask(Status status);
    }
}
