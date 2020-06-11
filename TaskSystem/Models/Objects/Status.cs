using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Перечисление статусов
    /// </summary>
    public enum Status
    {
        New = 1,
        InWork = 2,
        Paused = 3,
        Completed = 4,
        Canceled = 5
    }
}
