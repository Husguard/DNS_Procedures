using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

namespace TaskSystem.Dto
{
    /// <summary>
    /// Модель DTO для задания и его состояния
    /// </summary>
    [DataContract]
    public class ThemeDto
    {
        /// <summary>
        /// Идентификатор темы
        /// </summary>
        [DataMember(Name = "themeid")]
        public int Id { get; set; }

        /// <summary>
        /// Название темы
        /// </summary>
        [DataMember(Name = "themeName")]
        public string Name { get; set; }

        /// <summary>
        /// Конвертация модели бизнес-логики в модель данных
        /// </summary>
        /// <param name="theme">Объект темы</param>
        public ThemeDto(Theme theme)
        {
            Id = theme.Id;
            Name = theme.Name;
        }

        public ThemeDto() { }
    }
}
