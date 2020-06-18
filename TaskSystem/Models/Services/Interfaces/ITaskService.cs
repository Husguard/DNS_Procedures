﻿using System;
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
        /// Добавление новой версии задания
        /// </summary>
        /// <param name="moneyAward">Денежная награда</param>
        /// <param name="statusId">Новый статус</param>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponse AddTaskVersion(decimal moneyAward, WorkTaskStatus statusId, int taskId);

        /// <summary>
        /// Получение последней версии задания
        /// </summary>
        /// <param name="taskId">Идентификатор задания</param>
        ServiceResponseGeneric<WorkTaskDto> GetLastVersionOfTask(int taskId);

        /// <summary>
        /// Получение заданий, у которых определенный исполнитель
        /// </summary>
        /// <param name="performerId">Идентификатор исполнителя</param>
        ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByPerformer(int performerId);

        /// <summary>
        /// Получение заданий, у которых определенный создатель
        /// </summary>
        /// <param name="creatorId">Идентификатор создателя</param>
        ServiceResponseGeneric<IEnumerable<WorkTaskDto>> GetTasksByCreator(int creatorId);

    }
}