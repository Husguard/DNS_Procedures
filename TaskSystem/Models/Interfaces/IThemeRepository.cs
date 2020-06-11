using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    /// <summary>
    /// Интерфейс получения и добавления тем
    /// </summary>
    public interface IThemeRepository
    {
        /// <summary>
        /// Создание новой темы
        /// </summary>
        /// <param name="name">Название темы</param>
        void AddTheme(string name);

        /// <summary>
        /// Получение всех тем
        /// </summary>
        /// <returns></returns>
        IEnumerable<Theme> GetAllThemes();
        /// <summary>
        /// Получение тем, которые начинаются с введенной строки
        /// </summary>
        /// <param name="name">Часть названия темы</param>
        /// <returns></returns>
        IEnumerable<Theme> GetThemesByName(string name);
    }
}
