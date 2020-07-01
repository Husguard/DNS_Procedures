using System.Collections.Generic;
using TaskSystem.Models.Dto;

namespace TaskSystem.Models.Services.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса взаимодействия с темами
    /// </summary>
    public interface IThemeService
    {
        /// <summary>
        /// Получение всех тем
        /// </summary>
        ServiceResponse<IEnumerable<ThemeDto>> GetAllThemes();

        /// <summary>
        /// Получение всех тем, название которых начинается с ввода
        /// </summary>
        /// <param name="name">Название темы</param>
        ServiceResponse<IEnumerable<ThemeDto>> GetThemesByName(string name);

        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="name">Название новой темы</param>
        ServiceResponse AddTheme(string name);
    }
}
