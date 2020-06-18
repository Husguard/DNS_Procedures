using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Dto;
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
        ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetLastVersions();

        /// <summary>
        /// Метод получения последних версий заданий, у которых выбранный статус
        /// </summary>
        /// <param name="statusId"></param>
        ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByStatus(WorkTaskStatus status);

        /// <summary>
        /// Получение всех версий задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTaskByID(int taskId);

        /// <summary>
        /// Добавление нового задания
        /// </summary>
        /// <param name="task">Объект задания</param>
        ServiceResponse AddNewTask(WorkTaskDto task);

        /// <summary>
        /// Добавление новой версии задания, при принятии задания меняется только 
        /// награда от нового исполнителя, при других статусах используются старые значения
        /// ввиду этого нужна перегрузка метода
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Новый статус</param>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponse AddTaskVersion(int moneyAward, WorkTaskStatus statusId, int taskId);

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponseGeneric<WorkTaskDto> GetLastVersionOfTask(int taskId);
    }
}
