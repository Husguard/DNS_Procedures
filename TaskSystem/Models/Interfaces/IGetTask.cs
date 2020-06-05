using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface IGetTask
    {
        public IEnumerable<Task> GetAllTasks();
        public IEnumerable<Task> GetTasksByStatus(Status status);
        public IEnumerable<Task> GetTaskByVersion(TaskVersion version);
        public IEnumerable<Task> GetTaskByEmployee(Employee employee);
    }
}
