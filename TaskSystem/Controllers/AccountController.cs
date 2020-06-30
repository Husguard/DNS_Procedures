using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Dto;
using TaskSystem.Models;
using TaskSystem.Models.Services;
using TaskSystem.Models.Services.Implementations;
using TaskSystem.Models.Services.Interfaces;

namespace TaskSystem.Controllers
{
    /// <summary>
    /// Контроллер аутентификации работников
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Сервис взаимодействия с работниками
        /// </summary>
        private IEmployeeService _employeeService;
        public AccountController(IEmployeeService context)
        {
            _employeeService = context;
        }

        /// <summary>
        /// Метод получения представления для входа
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Метод входа пользователя, на основе введенного логина
        /// </summary>
        /// <param name="model">Данные работника</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                ServiceResponseGeneric<EmployeeDto> user = _employeeService.GetEmployeeByLogin(model.Login);
                if (user.Result != null)
                {
                    await Authenticate(user.Result);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Login", "Пользователя с таким логином не существует");
            }
            return View(model);
        }
        /// <summary>
        /// Метод получения представления для регистрации работника
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Метод регистрации нового работника
        /// </summary>
        /// <param name="model">Данные нового работника</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                ServiceResponseGeneric<EmployeeDto> user = _employeeService.GetEmployeeByLogin(model.Login);
                if (user.Result == null)
                {
                    _employeeService.RegisterNewEmployee(model);
                    ServiceResponseGeneric<EmployeeDto> newUser = _employeeService.GetEmployeeByLogin(model.Login);

                    await Authenticate(newUser.Result);

                    return RedirectToAction("Index", "Home");
                }
                else ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
            }
            return View(model);
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
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        /// <summary>
        /// Метод выхода из аккаунта пользователя
        /// </summary>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}