using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Репозиторий для получения и добавления комментариев
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly IConnectionDb _db;

        public CommentRepository(IConnectionDb db)
        {
            _db = db;
        }
        /// <summary>
        /// Метод получения всех комментариев к определенному заданию
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> GetCommentsOfTask(int taskId)
        {
            return _db.ExecuteReader(
               "TaskProcedureGetCommentsOfTask",
               (reader) => CreateComment(reader),
               new SqlParameter("@TaskID", taskId));
        }
        /// <summary>
        /// Метод получения всех комментариев от определенного работника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public IEnumerable<Comment> GetCommentsOfEmployee(int employeeId)
        {
            return _db.ExecuteReader(
              "TaskProcedureGetCommentsOfEmployee",
              (reader) => CreateComment(reader),
              new SqlParameter("@TaskID", employeeId));
        }
        /// <summary>
        /// Добавление комментария к заданию от работника
        /// </summary>
        /// <param name="message"></param>
        /// <param name="taskId"></param>
        /// <param name="employeeId"></param>
        public void AddCommentToTask(string message, int taskId, int employeeId)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureAddCommentToTask",
                new SqlParameter("@TaskID", employeeId),
                new SqlParameter("@EmployeeID", employeeId));
        }
        /// <summary>
        /// Метод создания объекта из данных от БД
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
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
