using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models.Dto;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Controllers
{
    [Authorize]
    [ApiController]
    public class ThemeControllerApi : ControllerBase
    {
        private readonly IThemeService _themeService;

        /// <summary>
        /// Инициализация сервисов
        /// </summary>
        /// <param name="themeService">Сервис тем</param>
        public ThemeControllerApi(IThemeService themeService)
        {
            _themeService = themeService;
        }

        /// <summary>
        /// Получение всех тем
        /// </summary>
        [HttpGet("GetAllThemes")]
        public ServiceResponse<IEnumerable<ThemeDto>> GetAllThemes() => _themeService.GetAllThemes();

        /// <summary>
        /// Получение тем, название которых начинаются с введенной строки
        /// </summary>
        /// <param name="name">Название темы</param>
        [HttpGet("GetThemesByName")]
        public ServiceResponse<IEnumerable<ThemeDto>> GetThemesByName(string name) => _themeService.GetThemesByName(name);


        /// <summary>
        /// Добавление новой темы
        /// </summary>
        /// <param name="themeDto">Название темы</param>
        [HttpPost("AddTheme")]
        public ServiceResponse AddTheme([FromBody] ThemeDto themeDto) => _themeService.AddTheme(themeDto.Name);
    }
}
