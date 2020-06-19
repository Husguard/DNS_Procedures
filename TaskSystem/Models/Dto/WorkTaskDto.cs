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
        [DataMember(Name = "themeId")]
        public int ThemeId { get; set; }

        /// <summary>
        /// Идентификатор работника-создателя
        /// </summary>
        [DataMember(Name = "creatorId")]
        public int CreatorId { get; set; }

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
        /// Идентификатор исполнителя
        /// </summary>
        [DataMember(Name = "performerId")]
        public int? PerformerId { get; set; }

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
        /// некоторые поля убрать бы
        public WorkTaskDto(WorkTask workTask)
        {
            Id = workTask.Id;
            Name = workTask.Name;
            Description = workTask.Description;
            ThemeId = workTask.ThemeId;
            CreateDate = workTask.CreateDate;
            CreateVersionDate = workTask.CreateVersionDate;
            ExpireDate = workTask.ExpireDate;
            Status = workTask.Status;
            CreatorId = workTask.CreatorId;
            PerformerId = workTask.PerformerId;
            MoneyAward = workTask.MoneyAward;
            Version = workTask.Version;
        }

        /// <summary>
        /// Пустой конструктор для принятия объекта со стороны клиента
        /// </summary>
        public WorkTaskDto() { }
    }
}
