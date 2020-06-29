using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using TaskSystem.Models.Objects;

namespace TaskSystem.Dto
{
    /// <summary>
    /// Модель DTO для задания и его состояния
    /// </summary>
    [DataContract]
    public class WorkTaskDto
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        [DataMember(Name = "taskId")]
        public int Id { get; set; }

        /// <summary>
        /// Название задания
        /// </summary>
        [DataMember(Name = "taskName")]
        public string Name { get; set; }

        /// <summary>
        /// Описание задания
        /// </summary>
        [DataMember(Name = "description")]
        [StringLength(300, ErrorMessage = "Описание не может быть длинее 300 символов")]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор темы задания
        /// </summary>
        [DataMember(Name = "theme")]
        public ThemeDto Theme { get; set; }

        /// <summary>
        /// Идентификатор работника-создателя
        /// </summary>
        [DataMember(Name = "creator")]
        public EmployeeDto Creator { get; set; }

        /// <summary>
        /// Версия задания, которая меняется при обновлении статуса
        /// </summary>
        [DataMember(Name = "version")]
        public byte Version { get; set; }

        /// <summary>
        /// Статус задания
        /// </summary>
        [DataMember(Name = "statusId")]
        public WorkTaskStatus Status { get; set; }

        /// <summary>
        /// Название статуса задания
        /// </summary>
        [DataMember(Name = "statusName")]
        public string StatusName { get; set; }

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
        /// Дата создания задания
        /// </summary>
        [DataMember(Name = "createDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата завершения задания
        /// </summary>
        [DataMember(Name = "expireDate")]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Дата создания версии
        /// </summary>
        [DataMember(Name = "createVersionDate")]
        public DateTime CreateVersionDate { get; set; }

        /// <summary>
        /// Конвертация модели бизнес-логики в модель данных
        /// </summary>
        /// <param name="workTask">Объект задания</param>
        public WorkTaskDto(WorkTask workTask)
        {
            Id = workTask.Id;
            Name = workTask.Name;
            Description = workTask.Description;
            Theme = new ThemeDto(workTask.Theme);
            CreateDate = workTask.CreateDate;
            CreateVersionDate = workTask.CreateVersionDate;
            ExpireDate = workTask.ExpireDate;
            Status = workTask.Status;
            StatusName = workTask.StatusName;
            Creator = new EmployeeDto(workTask.Creator);
            Performer = workTask.Performer == null ? null : new EmployeeDto(workTask.Performer);
            MoneyAward = workTask.MoneyAward;
            Version = workTask.Version;
        }

        /// <summary>
        /// Пустой конструктор для принятия объекта со стороны клиента
        /// </summary>
        public WorkTaskDto() { }
    }
}
