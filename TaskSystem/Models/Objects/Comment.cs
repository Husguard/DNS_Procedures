using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Модель, описывающая комментарий
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Идентификатор задания, с которым связан комментарий
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который оставил комментарий
        /// </summary>
        public int EmployeeId { get; set; }
        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Message { get; set; }
    }
}
