using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Interfaces
{
    public interface IThemeRepository
    {
        /// <summary>
        /// Создание новой темы
        /// </summary>
        /// <param name="name">Название темы</param>
        public void AddTheme(string name);
        /// <summary>
        /// Получение всех тем
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Theme> GetAllThemes();
        /// <summary>
        /// Получение всех тем по имени
        /// </summary>
        /// <param name="name">Название темы</param>
        /// <returns></returns>
        public IEnumerable<Theme> GetThemesByName(string name);
    }
}
