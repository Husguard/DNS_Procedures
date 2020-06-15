using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Objects;

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
        ServiceResponseGeneric<IEnumerable<Theme>> GetAllThemes();

        /// <summary>
        /// Получение всех тем, название которых начинается с ввода
        /// </summary>
        /// <param name="name">Название темы</param>
        ServiceResponseGeneric<IEnumerable<Theme>> GetThemesByName(string name);

        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="name">Название новой темы</param>
        ServiceResponse AddTheme(string name);
    }
}
