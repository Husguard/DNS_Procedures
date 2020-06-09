using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using TaskSystem.Models.Interfaces;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IConnectionDb _db;

        public TaskRepository(IConnectionDb db)
        {
            _db = db;
        }
        public IEnumerable<WorkTask> GetTaskByID(int taskid)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetTaskByID", 
                (reader) => CreateTask(reader), 
                new SqlParameter("@TaskID", taskid));
        }
        public IEnumerable<WorkTask> GetAllTasks()
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetAllTasks",
                (reader) => CreateTask(reader));
        }
        public IEnumerable<WorkTask> GetTasksByStatus(Status status)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetTaskByID", 
                (reader) => CreateTask(reader), 
                new SqlParameter("@StatusID", status));
        }
        public WorkTask GetTaskByVersion(int taskid, byte version)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetVersionOfTask", 
                (reader) => CreateTask(reader), 
                new SqlParameter("@TaskID", taskid), 
                new SqlParameter("@Version", version)
                ).First();
        }
        public IEnumerable<WorkTask> GetTasksByCreator(int creatorId)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetCreatorTasks",
                (reader) => CreateTask(reader), 
                new SqlParameter("@CreatorID", creatorId));
        }
        public IEnumerable<WorkTask> GetTasksByPerformer(int performerId)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetPerformerTasks",
                (reader) => CreateTask(reader), 
                new SqlParameter("@PerformerID", performerId));
        }
        public WorkTask GetLastVersionOfTask(int taskid)
        {
            return _db.ExecuteReader<WorkTask>(
                "TaskProcedureGetLastVersionOfTask",
                (reader) => CreateTask(reader),
                new SqlParameter("@TaskID", taskid)
                ).First();
        }
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
        public void UpdatePerformerOfTask(int taskId, Status statusId, byte version)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureUpdateStatusOfTask",
                new SqlParameter("@Version", version),
                new SqlParameter("@StatusID", statusId),
                new SqlParameter("@TaskID", taskId)
                );
        }
        private WorkTask CreateTask(IDataReader reader)
        {
            return new WorkTask()
            {
                ID = (int)reader["TaskID"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                CreatorID = (int)reader["CreatorID"],
        //        PerformerID = (int?)reader["PerformerID"], NULL
                ThemeID = (int)reader["ThemeID"],
                CreateDate = (DateTime)reader["CreateDate"],
                ExpireDate = (DateTime)reader["ExpireDate"]
            };
        }
    }
}
