using System.Collections.Generic;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Получение всех заданий и их версий
        /// </summary>
        /// <returns></returns>
        IEnumerable<WorkTask> GetAllTasks();
        /// <summary>
        /// Получение последних версий заданий
        /// </summary>
        IEnumerable<WorkTask> GetLastVersions();

        /// <summary>
        /// Получение последних версий заданий по статусу
        /// </summary>
        /// <param name="status">Статус</param>
        /// <returns></returns>
        IEnumerable<WorkTask> GetTasksByStatus(WorkTaskStatus status);

        /// <summary>
        /// Получение определенной версии, определенного задания
        /// </summary>
        /// <param name="version">Версия задания</param>
        /// <param name="taskId">Идентификатор задания</param>
        WorkTask GetTaskByVersion(int taskId, byte version);

        /// <summary>
        /// Получение заданий, у которых создатель выбранный работник
        /// </summary>
        /// <param name="creatorId">Создатель задания</param>
        IEnumerable<WorkTask> GetTasksByCreator(int creatorId);

        /// <summary>
        /// Получение заданий, у которых исполнитель выбранный работник
        /// </summary>
        /// <param name="performerId">Исполнитель задания</param>
        IEnumerable<WorkTask> GetTasksByPerformer(int performerId);

        /// <summary>
        /// Получение всех версий определенного задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        IEnumerable<WorkTask> GetTaskByID(int taskId);

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        WorkTask GetLastVersionOfTask(int taskId);

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        void AddNewTask(WorkTask task);

        /// <summary>
        /// Добавление новой версии задания
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Идентификатор статуса задания</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="performerID">Идентификатор исполнителя</param>
        void AddTaskVersion(decimal moneyAward, WorkTaskStatus statusId, int taskId, int performerID);
    }
}
