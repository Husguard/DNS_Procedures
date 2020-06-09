using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using TaskSystem.Models.Interfaces;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace TaskSystem.Models.Objects.Repositories
{
    /// <summary>
    /// Репозиторий получения и добавления заданий
    /// </summary>
    public class TaskRepository : ITaskRepository
    {
        private readonly IConnectionDb _db;

        public TaskRepository(IConnectionDb db)
        {
            _db = db;
        }
        /// <summary>
        /// Метод получения всех версий определенного задания
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTaskByID(int taskid)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetTaskByID", 
                (reader) => CreateTask(reader), 
                new SqlParameter("@TaskID", taskid));
        }
        /// <summary>
        /// Метод получения всех заданий и их версий
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetAllTasks()
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetAllTasks",
                (reader) => CreateTask(reader));
        }
        /// <summary>
        /// Метод получения последних версий заданий, у которых определенный статус
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByStatus(Status status)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetTaskByStatus", 
                (reader) => CreateTask(reader), 
                new SqlParameter("@StatusID", status));
        }
        /// <summary>
        /// Метод получения определенного задания определенной версии
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public WorkTask GetTaskByVersion(int taskid, byte version)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetVersionOfTask", 
                (reader) => CreateTask(reader), 
                new SqlParameter("@TaskID", taskid), 
                new SqlParameter("@Version", version)
                ).First();
        }
        /// <summary>
        /// Метод получения всех заданий, созданных определенным работником
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByCreator(int creatorId)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetCreatorTasks",
                (reader) => CreateTask(reader), 
                new SqlParameter("@CreatorID", creatorId));
        }
        /// <summary>
        /// Метод получения всех заданий, выполняемых определенным работником
        /// </summary>
        /// <param name="performerId"></param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByPerformer(int performerId)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetPerformerTasks",
                (reader) => CreateTask(reader), 
                new SqlParameter("@PerformerID", performerId));
        }
        /// <summary>
        /// Метод получения последней версии определенного задания
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public WorkTask GetLastVersionOfTask(int taskid)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetLastVersionOfTask",
                (reader) => CreateTask(reader),
                new SqlParameter("@TaskID", taskid)
                ).First();
        }
        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task"></param>
        public void AddNewTask(WorkTask task)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureAddTask",
                new SqlParameter("@Name", task.Name),
                new SqlParameter("@Description", task.Description),
                new SqlParameter("@ThemeID", task.ThemeID),
                new SqlParameter("@CreatorID", task.CreatorID),
                new SqlParameter("@ExpireDate", task.ExpireDate));
        }
        /// <summary>
        /// Добавление новой версии задания
        /// </summary>
        /// <param name="moneyAward"></param>
        /// <param name="statusId"></param>
        /// <param name="taskId"></param>
        /// <param name="performerID"></param>
        public void AddTaskVersion(int moneyAward, Status statusId, int taskId, int performerID)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureAddTaskVersion",
                new SqlParameter("@MoneyAward", moneyAward),
                new SqlParameter("@StatusID", statusId),
                new SqlParameter("@TaskID", taskId),
                new SqlParameter("@PerformerID", performerID)
                );
        }
        /// <summary>
        /// Изменение исполнителя у определенной версии определенного задания
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <param name="version"></param>
        public void UpdatePerformerOfTask(int taskId, int employeeId, byte version)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureUpdatePerformerOfTask",
                new SqlParameter("@Version", version),
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@TaskID", taskId)
                );
        }
        /// <summary>
        /// Изменение статуса у определенной версии определенного задания
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <param name="version"></param>
        public void UpdateStatusOfTask(int taskId, Status statusId, byte version)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureUpdateStatusOfTask",
                new SqlParameter("@Version", version),
                new SqlParameter("@StatusID", statusId),
                new SqlParameter("@TaskID", taskId)
                );
        }
        /// <summary>
        /// Метод создания объекта из данных от БД
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private WorkTask CreateTask(IDataReader reader)
        {
            return new WorkTask()
            {
                ID = (int)reader["TaskID"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                CreatorID = (int)reader["CreatorID"],
                PerformerID = reader["PerformerID"] as int?,
                ThemeID = (int)reader["ThemeID"],
                CreateDate = (DateTime)reader["CreateDate"],
                ExpireDate = (DateTime)reader["ExpireDate"],
                MoneyAward = reader["MoneyAward"] as decimal?
            };
        }
    }
}
