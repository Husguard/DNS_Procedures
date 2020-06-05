using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskStatus
    {
        public void AcceptTaskByEmployee(Task task, Employee employee);
        public void CancelTask(Task task);
        public void CompleteTask(Task task);
        public void PauseTask(Task task);
        public void ExpireTask(Task task);
        // в методы класса мб?
    }
}
