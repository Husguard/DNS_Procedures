using System;
using TaskSystem.Dto;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Модель, описывающая задание и его состояние
    /// </summary>
    public class WorkTask
    {
        /// <summary>
        /// Идентификатор задания
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название задания
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание задания
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор темы задания
        /// </summary>
        public int ThemeId { get; set; }

        /// <summary>
        /// Идентификатор работника-создателя
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Версия задания, которая меняется при обновлении статуса
        /// </summary>
        public byte Version { get; set; }

        /// <summary>
        /// Статус задания
        /// </summary>
        public WorkTaskStatus Status { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int? PerformerId { get; set; }

        /// <summary>
        /// Количество денежных знаков
        /// </summary>
        public decimal? MoneyAward { get; set; }

        /// <summary>
        /// Дата создания задания
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Дата завершения задания
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// Дата создания версии
        /// </summary>
        public DateTime CreateVersionDate { get; set; }

        /// <summary>
        /// Конвертация из модели данных в объект бизнес-модели
        /// </summary>
        /// <param name="workTaskDto">Объект данных задания</param>
        public WorkTask(WorkTaskDto workTaskDto) 
        {
            Id = workTaskDto.Id;
            Name = workTaskDto.Name;
            Description = workTaskDto.Description;
            ThemeId = workTaskDto.ThemeId;
            CreateDate = workTaskDto.CreateDate;
            CreateVersionDate = workTaskDto.CreateVersionDate;
            ExpireDate = workTaskDto.ExpireDate;
            Status = workTaskDto.Status;
            CreatorId = workTaskDto.CreatorId;
            PerformerId = workTaskDto.PerformerId;
            MoneyAward = workTaskDto.MoneyAward;
            Version = workTaskDto.Version;
        }

        /// <summary>
        /// Пустой конструктор для создания объекта в потоке БД
        /// </summary>
        public WorkTask() {}
    }
}
