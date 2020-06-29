using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TaskSystem.Dto;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Dto
{
    [DataContract]
    public class AddTaskVersionDto
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        [DataMember(Name = "taskId")]
        public int TaskId { get; set; }

        /// <summary>
        /// Статус задания
        /// </summary>
        [DataMember(Name = "statusId")]
        public WorkTaskStatus Status { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        [DataMember(Name = "performer")]
        public EmployeeDto Performer { get; set; }

        /// <summary>
        /// Количество денежных знаков
        /// </summary>
        [DataMember(Name = "moneyAward")]
        public decimal? MoneyAward { get; set; }

        /// <summary>
        /// Дата создания версии
        /// </summary>
        [DataMember(Name = "createVersionDate")]
        public DateTime CreateVersionDate { get; set; }
    }
}
