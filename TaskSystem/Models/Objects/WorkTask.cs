using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;

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
        public int ID { get; set; }
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
        public int ThemeID { get; set; }
        /// <summary>
        /// Идентификатор работника-создателя
        /// </summary>
        public int CreatorID { get; set; }
        /// <summary>
        /// Версия задания, которая меняется при обновлении статуса
        /// </summary>
        public byte Version { get; set; }
        /// <summary>
        /// Статус задания
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public int? PerformerID { get; set; }
        /// <summary>
        /// Количество денежных знаков
        /// </summary>
        public decimal? MoneyAward { get; set; }
        /// <summary>
        /// Дата создания задания?
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Дата завершения задания
        /// </summary>
        public DateTime ExpireDate { get; set; }
        public WorkTask()
        {

        }
    }
}
