using System.Runtime.Serialization;
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
        [DataMember(Name = "themeId")]
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

        /// <summary>
        /// Пустой конструктор для принятия объекта со стороны клиента
        /// </summary>
        public ThemeDto() { }
    }
}
