using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Модель темы
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Идентификатор темы
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Название темы
        /// </summary>
        public string Name { get; set; }
    }
}
