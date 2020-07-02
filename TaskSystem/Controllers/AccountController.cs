using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;
using TaskSystem.Models.Dto;
using TaskSystem.Models.Services;
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
        private readonly AccountManager _accountManager;

        public AccountController(AccountManager accountManager)
        {
            _accountManager = accountManager;
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
        public async Task<IActionResult> Login(LoginEmployee model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountManager.TryLogin(model)) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError("Login", "Пользователя с таким логином не существует");
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
        public async Task<IActionResult> Register(LoginEmployee model)
        {
            if (ModelState.IsValid)
            {
                if (await _accountManager.TryRegister(model)) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
            }
            return View(model);
        }

        /// <summary>
        /// Метод выхода из аккаунта пользователя
        /// </summary>
        public async Task<IActionResult> Logout()
        {
            await _accountManager.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}