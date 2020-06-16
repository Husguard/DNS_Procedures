using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskSystem.Dto;
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

        protected const string ThemeAlreadyExists = "Тема уже существует";
        protected const string ThemeTooLong = "Тема слишком длинная, ограничение в 100 символов";

        public ThemeService(IThemeRepository themeRepository, ITaskRepository taskRepository, IEmployeeRepository employeeRepository, ILoggerFactory logger)
            : base(taskRepository, employeeRepository, logger)
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
                return ServiceResponse.Warning(ThemeAlreadyExists);
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
                _logger.LogWarning("Theme with Name = {0} is exists", name);
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
                _logger.LogWarning("Theme with Length = {0} is incorrect", name.Length);
                return true;
            }
            return false;
        }
    }
}


