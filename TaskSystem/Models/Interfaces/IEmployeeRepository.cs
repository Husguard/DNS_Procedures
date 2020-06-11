using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Interfaces
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Принятие задания на выполнение работником за определенную награду, создаётся новая версия задания
        /// </summary>
        /// <param name="employeeId">Идентификатор нового исполнителя</param>
        /// <param name="taskId">Идентификатор задания</param>
        void AcceptTaskByEmployee(int employeeId, int taskId);
    }
}
