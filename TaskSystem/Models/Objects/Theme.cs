using TaskSystem.Dto;

namespace TaskSystem.Models.Objects
{
    /// <summary>
    /// Модель темы
    /// </summary>
    public class Theme
    {
        /// <summary>
        /// Идентификатор темы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название темы
        /// </summary>
        public string Name { get; set; }
    }
}
