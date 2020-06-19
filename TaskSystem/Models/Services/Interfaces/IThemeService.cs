using System.Collections.Generic;
using TaskSystem.Dto;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Интерфейс сервиса взаимодействия с темами
    /// </summary>
    public interface IThemeService
    {
        /// <summary>
        /// Получение всех тем
        /// </summary>
        ServiceResponseGeneric<IEnumerable<ThemeDto>> GetAllThemes();

        /// <summary>
        /// Получение всех тем, название которых начинается с ввода
        /// </summary>
        /// <param name="name">Название темы</param>
        ServiceResponseGeneric<IEnumerable<ThemeDto>> GetThemesByName(string name);

        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="name">Название новой темы</param>
        ServiceResponse AddTheme(string name);
    }
}
