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
        /// <summary>
        /// Агрегация класса подключения к БД
        /// </summary>
        /// <param name="db">Класс подключения к БД</param>
        public TaskRepository(IConnectionDb db)
        {
            _db = db;
            var l = GetTasksByStatus(Status.Completed);
        }
        /// <summary>
        /// Метод получения всех версий определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTaskByID(int taskId)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetTaskByID",
                WorkTaskFromReader,
                new SqlParameter("@TaskID", taskId));
        }
        /// <summary>
        /// Метод получения всех заданий и их версий
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetAllTasks()
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetAllTasks",
                WorkTaskFromReader);
        }
        /// <summary>
        /// Метод получения последних версий заданий, у которых определенный статус
        /// </summary>
        /// <param name="status">Номер статуса</param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByStatus(Status status)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetTaskByStatus",
                WorkTaskFromReader,
                new SqlParameter("@StatusID", status));
        }
        /// <summary>
        /// Метод получения определенного задания определенной версии
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="version">Версия задания</param>
        /// <returns></returns>
        public WorkTask GetTaskByVersion(int taskId, byte version)
        {
            return _db.ExecuteReaderGetSingle<WorkTask>(
                "TaskProcedureGetVersionOfTask",
                WorkTaskFromReader,
                new SqlParameter("@TaskID", taskId),
                new SqlParameter("@Version", version)
                );
        }
        /// <summary>
        /// Метод получения всех заданий, созданных определенным работником
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByCreator(int creatorId)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetCreatorTasks",
                WorkTaskFromReader,
                new SqlParameter("@CreatorID", creatorId));
        }
        /// <summary>
        /// Метод получения всех заданий, выполняемых определенным работником
        /// </summary>
        /// <param name="performerId"></param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByPerformer(int performerId)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetPerformerTasks",
                WorkTaskFromReader,
                new SqlParameter("@PerformerID", performerId));
        }
        /// <summary>
        /// Метод получения последней версии определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        /// <returns></returns>
        public WorkTask GetLastVersionOfTask(int taskId)
        {
            return _db.ExecuteReaderGetSingle<WorkTask>(
                "TaskProcedureGetLastVersionOfTask",
                WorkTaskFromReader,
                new SqlParameter("@TaskID", taskId)
                );
        }
        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания, созданный клиентом</param>
        public void AddNewTask(WorkTask task)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureAddTask",
                new SqlParameter("@Name", task.Name),
                new SqlParameter("@Description", task.Description),
                new SqlParameter("@ThemeID", task.ThemeId),
                new SqlParameter("@CreatorID", task.CreatorId),
                new SqlParameter("@ExpireDate", task.ExpireDate));
        }
        /// <summary>
        /// Добавление новой версии задания
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Идентификатор статуса</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="performerID">Идентификатор исполнителя</param>
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
        public IEnumerable<WorkTask> GetLastVersions()
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetLastVersions",
                WorkTaskFromReader
                );
        }
        /// <summary>
        /// Метод создания объекта из данных от БД
        /// </summary>
        /// <param name="reader">Поток чтения данных из БД</param>
        /// <returns></returns>
        private WorkTask WorkTaskFromReader(IDataReader reader)
        {
            return new WorkTask()
            {
                Id = (int)reader["TaskID"],
                Name = (string)reader["Name"],
                Description = (string)reader["Description"],
                CreatorId = (int)reader["CreatorID"],
                PerformerId = reader["PerformerID"] as int?,
                ThemeId = (int)reader["ThemeID"],
                CreateDate = (DateTime)reader["CreateDate"],
                ExpireDate = (DateTime)reader["ExpireDate"],
                MoneyAward = reader["MoneyAward"] as decimal?
            };
        }
    }
}
