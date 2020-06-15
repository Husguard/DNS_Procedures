using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с темами
    /// </summary>
    public class ThemeService : BaseService, IThemeService
    {
        private readonly IThemeRepository _themeRepository;
        private readonly ILogger _logger;

        public ThemeService(IThemeRepository themeRepository, ILogger logger)
        {
            _themeRepository = themeRepository;
            _logger = logger;
        }
        /// <summary>
        /// Получение всех тем
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<Theme>> GetAllThemes()
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var themes = _themeRepository.GetAllThemes();
                return ServiceResponseGeneric<IEnumerable<Theme>>.Success(themes);
            });
        }

        /// <summary>
        /// Получение всех тем, название которых начинается с ввода
        /// </summary>
        /// <param name="name">Название темы</param>
        public ServiceResponseGeneric<IEnumerable<Theme>> GetThemesByName(string name)
        {
            return ExecuteWithCatchGeneric(() =>
            {
                var themes = _themeRepository.GetThemesByName(name);
                return ServiceResponseGeneric<IEnumerable<Theme>>.Success(themes);
            });
        }

        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="name">Название новой темы</param>
        public ServiceResponse AddTheme(string name)
        {
            return ExecuteWithCatch(() =>
            {
                // проверка существования темы
                _themeRepository.AddTheme(name);
                return ServiceResponse.Warning(ServiceWarningMessages.ThemeAlreadyExists);
            });
        }
    }
}


