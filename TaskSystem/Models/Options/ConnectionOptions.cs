using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Options
{
    /// <summary>
    /// Класс для хранения строки подключения
    /// </summary>
    public class ConnectionStringOptions
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string DefaultConnection { get; set; }
    }
}
