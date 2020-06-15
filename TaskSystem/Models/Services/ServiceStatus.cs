using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Перечисление статусов выполнения запроса
    /// </summary>
    public enum ServiceStatus : byte
    {
        Success = 1,
        Warning = 2,
        Fail = 3
    }
}
