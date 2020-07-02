using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskSystem.Models.Dto;
using TaskSystem.Models.Repositories.Interfaces;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Models.Services.Implementations
{
    /// <summary>
    /// Сервис проверки и выполнения запросов, связанных с темами
    /// </summary>
    public class ThemeService : IThemeService
    {
        private readonly IThemeRepository _themeRepository;
        private readonly BaseService _baseService;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="themeRepository">Репозиторий тем</param>
        public ThemeService(IThemeRepository themeRepository, BaseService baseService)
        {
            _baseService = baseService;
            _themeRepository = themeRepository;
        }
        /// <summary>
        /// Получение всех тем
        /// </summary>
        public ServiceResponse<IEnumerable<ThemeDto>> GetAllThemes()
        {
            return _baseService.ExecuteWithCatch(() =>
            {
                var themes = _themeRepository.GetAllThemes();

                return ServiceResponse<IEnumerable<ThemeDto>>.Success(
                    themes.Select((theme) => new ThemeDto(theme)));
            });
        }

        /// <summary>
        /// Получение всех тем, название которых начинается с ввода
        /// </summary>
        /// <param name="name">Название темы</param>
        public ServiceResponse<IEnumerable<ThemeDto>> GetThemesByName(string name)
        {
            return _baseService.ExecuteWithCatch(() =>
            {

                if (ThemeIsTooLong(name))
                    return ServiceResponse<IEnumerable<ThemeDto>>.Warning("Тема слишком длинная, ограничение в 100 символов");

                if(ThemeIsNotExists(name))
                    return ServiceResponse<IEnumerable<ThemeDto>>.Warning("Такой темы не существует, добавьте ее");

                var themes = _themeRepository.GetThemesByName(name);
                return ServiceResponse<IEnumerable<ThemeDto>>.Success(
                    themes.Select((theme) => new ThemeDto(theme)));
            });
        }

        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="name">Название новой темы</param>
        public ServiceResponse AddTheme(string name)
        {
            return _baseService.ExecuteWithCatch(() =>
            {
                if(IsEmptyName(name))
                    return ServiceResponse.Warning("Введите название темы");

                if (ThemeIsTooLong(name))
                    return ServiceResponse.Warning("Тема слишком длинная, ограничение в 100 символов");

                if(ThemeIsExists(name))
                    return ServiceResponse.Warning("Тема уже существует");

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
            return (!_themeRepository.GetThemesByName(name).Any());
        }

        /// <summary>
        /// Метод проверки длины темы
        /// </summary>
        /// <param name="name">Название темы</param>
        private bool ThemeIsTooLong(string name)
        {
          return (name.Length > 100);
        }

        private bool IsEmptyName(string name)
        {
            return string.IsNullOrWhiteSpace(name);
        }
    }
}


