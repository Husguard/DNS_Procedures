using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public List<Task> Tasks { get; set; }
        public TaskRepository()
        {
            Tasks = new List<Task>();
        }
        public Task GetTaskByID(int taskid)
        {
            return Tasks.Find((task) => task.ID == taskid);
        }
        public IEnumerable<Task> GetAllTasks()
        {
            return Tasks;
        }
        public IEnumerable<Task> GetTasksByStatus(Status status)
        {
            return Tasks.Where((task) => task.TaskVersions.Last().Status == status);
        }
        public IEnumerable<Task> GetTaskByVersion(int version)
        {
            return Tasks.Where((task) => task.TaskVersions.Last().Version == version);
        }
        public IEnumerable<Task> GetTaskByEmployee(Employee employee)
        {
            return Tasks.Where((task) => task.TaskVersions.Last().Performer == employee);
        }
        public void AddNewTask(Task task)
        {
            Tasks.Add(task);
        }
    }
}
