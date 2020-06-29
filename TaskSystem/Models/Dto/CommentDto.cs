using System.Runtime.Serialization;
using TaskSystem.Models.Objects;

namespace TaskSystem.Dto
{
    /// <summary>
    /// Модель DTO для комментария
    /// </summary>
    [DataContract]
    public class CommentDto
    {
        /// <summary>
        /// Идентификатор задания, с которым связан комментарий
        /// </summary>
        [DataMember(Name = "taskId")]
        public int TaskId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, который оставил комментарий
        /// </summary>
        [DataMember(Name = "employeeId")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// Имя пользователя, который оставил комментарий
        /// </summary>
        [DataMember(Name = "employeeName")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }


        /// <summary>
        /// Конвертация модели бизнес-логики в модель данных
        /// </summary>
        /// <param name="comment">Объект комментария</param>
        public CommentDto(Comment comment)
        {
            TaskId = comment.TaskId;
            EmployeeId = comment.Employee.Id;
            EmployeeName = comment.Employee.Name;
            Message = comment.Message;
        }

        /// <summary>
        /// Пустой конструктор для принятия объекта со стороны клиента
        /// </summary>
        public CommentDto() { }
    }
}
