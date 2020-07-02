using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskSystem.Models.Dto;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Models
{
    /// <summary>
    /// Класс аутентификации работников
    /// </summary>
    public class AccountManager
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHttpContextAccessor _httpContext;

        public AccountManager(IEmployeeService employeeService, IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Метод попытки зарегестировать нового работника
        /// </summary>
        /// <param name="model">Данные нового работника</param>
        public async Task<bool> TryRegister(LoginEmployee model)
        {
            ServiceResponse<EmployeeDto> user = _employeeService.GetEmployeeByLogin(model.Login);
            if (user.Result == null)
            {
                _employeeService.RegisterNewEmployee(model);
                ServiceResponse<EmployeeDto> newUser = _employeeService.GetEmployeeByLogin(model.Login);

                await Authenticate(newUser.Result);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод попытки войти в аккаунт работника
        /// </summary>
        /// <param name="model">Данные работника</param>
        public async Task<bool> TryLogin(LoginEmployee model)
        {
            ServiceResponse<EmployeeDto> user = _employeeService.GetEmployeeByLogin(model.Login);
            if (user.Result != null)
            {
                await Authenticate(user.Result);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод аутентификации работника
        /// </summary>
        /// <param name="employee">Данные работника</param>
        private async Task Authenticate(EmployeeDto employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie");
            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        /// <summary>
        /// Метод выхода из аккаунта пользователя
        /// </summary>
        public async Task Logout()
        {
            await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
