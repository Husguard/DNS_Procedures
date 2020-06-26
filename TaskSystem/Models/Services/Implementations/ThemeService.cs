using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;

namespace TaskSystem.Models.Services
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с темами
    /// </summary>
    public class ThemeService : BaseService, IThemeService
    {
        private readonly IThemeRepository _themeRepository;

        private const string ThemeAlreadyExists = "Тема уже существует";
        private const string ThemeTooLong = "Тема слишком длинная, ограничение в 100 символов";
        private const string ThemeNotExists = "Выбранной темы не существует, добавьте ее";

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="themeRepository">Репозиторий тем</param>
        /// <param name="logger">Инициализатор логгера</param>
        public ThemeService(IThemeRepository themeRepository, ILoggerFactory logger, UserManager manager)
            : base(logger, manager)
        {
            _themeRepository = themeRepository;
        }
        /// <summary>
        /// Получение всех тем
        /// </summary>
        public ServiceResponseGeneric<IEnumerable<ThemeDto>> GetAllThemes()
        {
            return ExecuteWithCatch(() =>
            {
                var themes = _themeRepository.GetAllThemes();
                return ServiceResponseGeneric<IEnumerable<ThemeDto>>.Success(
                    themes.Select((theme) => new ThemeDto(theme)));
            });
        }

        /// <summary>
        /// Получение всех тем, название которых начинается с ввода
        /// </summary>
        /// <param name="name">Название темы</param>
        public ServiceResponseGeneric<IEnumerable<ThemeDto>> GetThemesByName(string name)
        {
            return ExecuteWithCatch(() =>
            {
                if (ThemeIsTooLong(name))
                    return ServiceResponseGeneric<IEnumerable<ThemeDto>>.Warning(ThemeTooLong);
                if(ThemeIsNotExists(name))
                    return ServiceResponseGeneric<IEnumerable<ThemeDto>>.Warning(ThemeNotExists);
                var themes = _themeRepository.GetThemesByName(name);
                return ServiceResponseGeneric<IEnumerable<ThemeDto>>.Success(
                    themes.Select((theme) => new ThemeDto(theme)));
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
                if (ThemeIsTooLong(name))
                    return ServiceResponse.Warning(ThemeTooLong);
                if(ThemeIsExists(name))
                    return ServiceResponse.Warning(ThemeAlreadyExists);
                _themeRepository.AddTheme(name);
                return ServiceResponse.Success();
            });
        }

        /// <summary>
        /// Метод проверки существования темы с введенным названием
        /// </summary>
        /// <param name="name">Название темы</param>
        private bool ThemeIsExists(string name)
        {
            if (_themeRepository.GetThemesByName(name)
                .Select(
                    (theme) => theme.Name == name).Any())
            {
                _logger.LogWarning("Тема с названием {0} уже существует", name);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки отсутствия темы при ее поиске
        /// </summary>
        /// <param name="name">Название темы</param>
        private bool ThemeIsNotExists(string name)
        {
            if (_themeRepository.GetThemesByName(name) == null)
            {
                _logger.LogWarning("Темы с названием {0} не существует", name);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод проверки длины темы
        /// </summary>
        /// <param name="name">Название темы</param>
        private bool ThemeIsTooLong(string name)
        {
            if (name.Length > 100)
            {
                _logger.LogWarning("Длина введенной темы слишком большая '{0}'", name.Length);
                return true;
            }
            return false;
        }
    }
}


