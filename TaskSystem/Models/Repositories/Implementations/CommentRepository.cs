using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Репозиторий для получения и добавления комментариев
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly IConnectionDb _db;

        /// <summary>
        /// Агрегация класса подключения к БД
        /// </summary>
        /// <param name="db">Класс подключения к БД</param>
        public CommentRepository(IConnectionDb db)
        {
            _db = db;
        }

        /// <summary>
        /// Метод получения всех комментариев к определенному заданию
        /// </summary>
        /// <param name="taskId"></param>
        public IEnumerable<Comment> GetCommentsOfTask(int taskId)
        {
            return _db.ExecuteReaderGetList(
               "TaskProcedureGetCommentsOfTask",
               CommentFromReader,
               new SqlParameter("@TaskID", taskId));
        }

        /// <summary>
        /// Метод получения всех комментариев от определенного работника
        /// </summary>
        /// <param name="employeeId"></param>
        public IEnumerable<Comment> GetCommentsOfEmployee(int employeeId)
        {
            return _db.ExecuteReaderGetList(
              "TaskProcedureGetCommentsOfEmployee",
              CommentFromReader,
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
        /// <param name="reader">Поток чтения данных</param>
        private Comment CommentFromReader(IDataReader reader)
        {
            return new Comment()
            {
                TaskId = (int)reader["TaskID"],
                EmployeeId = (int)reader["CreatorID"],
                Message = (string)reader["Message"]
            };
        }
    }
}
