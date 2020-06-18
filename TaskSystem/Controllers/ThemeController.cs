using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models.Interfaces;
using TaskSystem.Models.Objects;
using TaskSystem.Models.Services;

namespace TaskSystem.Controllers
{
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService _themeService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="themeService">Сервис тем</param>
        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        /// <summary>
        /// Получение всех тем
        /// </summary>
        [HttpGet]
        [Route("GetAllThemes")]
        public ServiceResponseGeneric<IEnumerable<ThemeDto>> GetAllThemes() => _themeService.GetAllThemes();

        /// <summary>
        /// Получение тем, название которых начинаются с введенной строки
        /// </summary>
        /// <param name="name">Название темы</param>
        [HttpGet]
        [Route("GetThemesByName")]
        public ServiceResponseGeneric<IEnumerable<ThemeDto>> GetThemesByName(string name) => _themeService.GetThemesByName(name);


        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="themeDto">Название темы</param>
        [HttpPost]
        [Route("AddTheme")]
        public ServiceResponse AddTheme(ThemeDto themeDto) => _themeService.AddTheme(themeDto.Name);
    }
}
