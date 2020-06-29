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
        public Theme Theme { get; set; }

        /// <summary>
        /// Идентификатор работника-создателя
        /// </summary>
        public Employee Creator { get; set; }

        /// <summary>
        /// Версия задания, которая меняется при обновлении статуса
        /// </summary>
        public byte Version { get; set; }

        /// <summary>
        /// Статус задания
        /// </summary>
        public WorkTaskStatus Status { get; set; }

        /// <summary>
        /// Название статуса задания
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public Employee Performer { get; set; }

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
        /// Пустой конструктор для создания объекта в потоке БД
        /// </summary>
        public WorkTask() {}
    }
}
