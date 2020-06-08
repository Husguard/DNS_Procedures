using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Interfaces
{
    public interface IConnectionRepository : ITaskRepository, ICommentRepository, IThemeRepository
    {
        public void AddTask(Task task);
        public void AddCommentToTask();
    }
}
