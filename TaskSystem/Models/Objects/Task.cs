using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class Task : ITaskStatus
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Theme Theme { get; set; }
        public Employee Creator { get; set; }
        public List<TaskVersion> TaskVersions { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Task(string name, string description, Theme theme, Employee creator, DateTime expireDate)
        {
            // ID нового где брать?
            Name = name;
            Description = description;
            Theme = theme;
            Creator = creator;
            CreateDate = DateTime.Now;
            ExpireDate = expireDate;
            TaskVersions = new List<TaskVersion>();
            TaskVersions.Add(
                new TaskVersion()
                { 
                    PerformerID = null,
                    Performer = null,
                    MoneyAward = null,
                    Task = this,
                    Version = 0,
                    Status = Status.New
                });
        }
        public void AcceptTaskByEmployee(Employee employee, decimal award)
        {
            TaskVersions.Add(
                new TaskVersion()
                {
                    PerformerID = employee.ID,
                    Performer = employee,
                    MoneyAward = award,
                    Task = this,
                    Version = (byte)TaskVersions.Count(),
                    Status = Status.InWork
                });
        }
        // пересмотреть методы, можно только update оставить
        public void CancelTask()
        {
            UpdateStatusOfTask(Status.Canceled);
        }
        public void CompleteTask()
        {
            UpdateStatusOfTask(Status.Completed);
        }
        public void PauseTask()
        {
            UpdateStatusOfTask(Status.Paused);
        }
        public bool IsTaskExpire()
        {
            if (ExpireDate > DateTime.Now)
            {
                UpdateStatusOfTask(Status.Canceled);
                return true;
            }
            return false;
        }
        public void UpdateStatusOfTask(Status status)
        {
            TaskVersions.Last().Status = status;
        }
    }
}
