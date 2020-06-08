using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class CommentRepository : ICommentRepository
    {
        public List<Comment> Comments { get; set; }
        public IEnumerable<Comment> GetCommentsOfTask(Task task)
        {
            return Comments.Where((comment) => comment.Task == task);
        }
        public IEnumerable<Comment> GetCommentsOfEmployee(Employee employee)
        {
            return Comments.Where((comment) => comment.Employee == employee);
        }
        public void AddCommentToTask(string message, Task task, Employee employee)
        {
            Comments.Add(new Comment() { Message = message, Task = task, Employee = employee }); // стоит ли так делать?
        }
    }
}
