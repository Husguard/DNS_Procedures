using TaskSystem.Dto;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Модель темы
    /// </summary>
    public class Theme
    {
        public Theme(ThemeDto theme)
        {
            Id = theme.Id;
            Name = theme.Name;
        }

        /// <summary>
        /// Идентификатор темы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название темы
        /// </summary>
        public string Name { get; set; }

        public Theme() { }
    }
}
