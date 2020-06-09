using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Получение всех заданий и их версий
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetAllTasks();
        /// <summary>
        /// Получение последней версии заданий по статусу
        /// </summary>
        /// <param name="status">Статус</param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByStatus(Status status);
        /// <summary>
        /// Получение определенной версии, определенного задания
        /// </summary>
        /// <param name="version">Версия задания</param>
        /// <returns></returns>
        public WorkTask GetTaskByVersion(int taskid, byte version);
        /// <summary>
        /// Получение заданий, у которых создатель выбранный работник
        /// </summary>
        /// <param name="creatorId">Создатель задания</param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByCreator(int creatorId);
        /// <summary>
        /// Получение заданий, у которых исполнитель выбранный работник
        /// </summary>
        /// <param name="performerId">Исполнитель задания</param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTasksByPerformer(int performerId);
        /// <summary>
        /// Получение всех версий определенного задания
        /// </summary>
        /// <param name="taskid">ID задания</param>
        /// <returns></returns>
        public IEnumerable<WorkTask> GetTaskByID(int taskid);
        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public WorkTask GetLastVersionOfTask(int taskId);
        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        void AddNewTask(WorkTask task);
        /// <summary>
        /// Изменение исполнителя у определенной версии определенного задания
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <param name="version"></param>
        void UpdatePerformerOfTask(int taskId, int employeeId, byte version);
        /// <summary>
        /// Изменение статуса у определенной версии определенного задания
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <param name="version"></param>
        void UpdateStatusOfTask(int taskId, Status statusId, byte version)
    }
}
