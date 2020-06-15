using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services;

namespace TaskSystem.Models.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса взаимодействия с заданиями
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Метод получения последних версий всех заданий
        /// </summary>
        ServiceResponseGeneric<IEnumerable<WorkTask>> GetLastVersions();

        /// <summary>
        /// Метод получения последних версий заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId"></param>
        ServiceResponseGeneric<IEnumerable<WorkTask>> GetTasksByStatus(WorkTaskStatus status);

        /// <summary>
        /// Получение всех версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponseGeneric<IEnumerable<WorkTask>> GetTaskByID(int taskId);

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        ServiceResponse AddNewTask(WorkTask task);

        /// <summary>
        /// Добавление новой версии задания, при принятии задания меняется только 
        /// награда от нового исполнителя, при других статусах используются старые значения
        /// ввиду этого нужна перегрузка метода
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Новый статус</param>
        /// <param name="taskId">Идентификатор задания</param>
        /// <param name="performerID">Идентификатор исполнителя</param>
        ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId, int performerID);

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponseGeneric<WorkTask> GetLastVersionOfTask(int taskId);
    }
}
