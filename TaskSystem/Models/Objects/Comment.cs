using System;

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
        public Employee Employee { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дата создания комментария
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
