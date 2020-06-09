using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConnectionDb _db;

        public CommentRepository(IConnectionDb db)
        {
            _db = db;
        }
        public IEnumerable<Comment> GetCommentsOfTask(int taskId)
        {
            return _db.ExecuteReader(
               "TaskProcedureGetCommentsOfTask",
               (reader) => CreateComment(reader),
               new SqlParameter("@TaskID", taskId));
        }
        public IEnumerable<Comment> GetCommentsOfEmployee(int employeeId)
        {
            return _db.ExecuteReader(
              "TaskProcedureGetCommentsOfEmployee",
              (reader) => CreateComment(reader),
              new SqlParameter("@TaskID", employeeId));
        }
        public void AddCommentToTask(string message, int taskId, int employeeId)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureAddCommentToTask",
                new SqlParameter("@TaskID", employeeId),
                new SqlParameter("@EmployeeID", employeeId));
        }
        private Comment CreateComment(IDataReader reader)
        {
            return new Comment()
            {
                TaskID = (int)reader["TaskID"],
                EmployeeID = (int)reader["CreatorID"],
                Message = (string)reader["Message"]
            };
        }
    }
}
