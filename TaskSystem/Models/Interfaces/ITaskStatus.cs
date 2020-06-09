using System;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface ITaskStatus
    {
        /// <summary>
        /// Принятие задания на выполнение работником за определенную награду, создаётся новая версия задания
        /// </summary>
        /// <param name="employee">Объект работника</param>
        /// <param name="award">Награда за выполнение</param>
        public void AcceptTaskByEmployee(int employeeId, int taskId);
        /// <summary>
        /// Обновление статуса у последней версии задания
        /// </summary>
        /// <param name="status">Статус задания</param>
        public void UpdateStatusOfTask(Status status);
    }
}
