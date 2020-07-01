using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Repositories.Interfaces;

namespace TaskSystem.Models.Repositories.Implementations
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
        }

        /// <summary>
        /// Метод получения всех версий определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public IEnumerable<WorkTask> GetTaskByID(int taskId)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetTaskByID",
                WorkTaskFromReader,
                new SqlParameter("@TaskID", taskId)
                );
        }

        /// <summary>
        /// Метод получения всех заданий и их версий
        /// </summary>
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
        public IEnumerable<WorkTask> GetTasksByStatus(WorkTaskStatus status)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetTasksByStatus",
                WorkTaskFromReader,
                new SqlParameter("@StatusID", status)
                );
        }

        /// <summary>
        /// Метод получения определенного задания определенной версии
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="version">Версия задания</param>
        public WorkTask GetTaskByVersion(int taskId, byte version)
        {
            return _db.ExecuteReaderGetSingle<WorkTask>(
                "TaskProcedureGetVersionOfTask",
                WorkTaskFromReader,
                new SqlParameter("@TaskID", taskId),
                new SqlParameter("@Version", version)
                ) ?? throw new EmptyResultException("Версия не была найдена");
        }

        /// <summary>
        /// Метод получения всех заданий, созданных определенным работником
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        public IEnumerable<WorkTask> GetTasksByCreator(int creatorId)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetCreatorTasks",
                WorkTaskFromReader,
                new SqlParameter("@CreatorID", creatorId)
                );
        }

        /// <summary>
        /// Метод получения всех заданий, выполняемых определенным работником
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        public IEnumerable<WorkTask> GetTasksByPerformer(int performerId)
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetPerformerTasks",
                WorkTaskFromReader,
                new SqlParameter("@PerformerID", performerId)
                );
        }
        /// <summary>
        /// Метод получения последней версии определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        public WorkTask GetLastVersionOfTask(int taskId)
        {
            return _db.ExecuteReaderGetSingle<WorkTask>(
                "TaskProcedureGetLastVersionOfTask",
                WorkTaskFromReader,
                new SqlParameter("@TaskID", taskId)
                ) ?? throw new EmptyResultException("Задание не было найдено");
        }

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания, созданный клиентом</param>
        public void AddNewTask(string name, string description, int themeId, int creatorId, DateTime expireDate)
        {
            _db.ExecuteNonQuery(
                "TaskProcedureAddTask",
                new SqlParameter("@Name", name),
                new SqlParameter("@Description", description),
                new SqlParameter("@ThemeID", themeId),
                new SqlParameter("@CreatorID", creatorId),
                new SqlParameter("@ExpireDate", expireDate));
        }

        /// <summary>
        /// Добавление новой версии задания
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Идентификатор статуса</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="performerID">Идентификатор исполнителя</param>
        public void AddTaskVersion(decimal moneyAward, WorkTaskStatus statusId, int taskId, int performerID)
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
        /// Получение последних версий всех заданий
        /// </summary>
        public IEnumerable<WorkTask> GetLastVersions()
        {
            return _db.ExecuteReaderGetList<WorkTask>(
                "TaskProcedureGetLastVersions",
                WorkTaskFromReader
                ) ?? throw new EmptyResultException("Задания не были найдены");
        }

        /// <summary>
        /// Метод создания объекта из данных от БД
        /// </summary>
        /// <param name="reader">Класс чтения потока данных из БД</param>
        private WorkTask WorkTaskFromReader(IDataReader reader)
        {
            WorkTask task = new WorkTask()
            {
                Id = (int)reader["TaskID"],
                Name = (string)reader["TaskName"],
                Description = (string)reader["Description"],
                Creator = new Employee
                {
                    Id = (int)reader["CreatorID"],
                    Name = (string)reader["CreatorName"]
                },
                Theme = new Theme
                {
                    Id = (int)reader["ThemeID"],
                    Name = (string)reader["ThemeName"]
                },
                CreateDate = (DateTime)reader["CreateDate"],
                ExpireDate = (DateTime)reader["ExpireDate"],
                MoneyAward = (decimal)reader["MoneyAward"],
                Status = (WorkTaskStatus)reader["StatusID"],
                StatusName = (string)reader["StatusName"],
                CreateVersionDate = (DateTime)reader["CreateVersionDate"]
            };
            if (reader["PerformerID"] != DBNull.Value)
            {
                task.Performer = new Employee
                {
                    Id = (int)reader["PerformerID"],
                    Name = (string)reader["PerformerName"]
                };
            }
            return task;
        }
    }
}
