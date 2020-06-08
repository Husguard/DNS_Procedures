using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskRepository
    {
        public IEnumerable<Task> GetAllTasks();
        public IEnumerable<Task> GetTasksByStatus(Status status);
        public IEnumerable<Task> GetTaskByVersion(int version);
        public IEnumerable<Task> GetTaskByEmployee(Employee employee);
        public Task GetTaskByID(int taskid);
        public void AddNewTask(Task task);
    }
}
