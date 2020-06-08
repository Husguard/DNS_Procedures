using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetCommentsOfTask(Task task);
        public IEnumerable<Comment> GetCommentsOfEmployee(Employee employee);
        public void AddCommentToTask(string message, Task task, Employee employee);
    }
}
