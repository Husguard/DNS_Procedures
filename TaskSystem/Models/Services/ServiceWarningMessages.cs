using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSystem.Models.Services
{
    public class ServiceWarningMessages
    {
        // группировать ошибки(проверка инпутов, роль, существование уже)
        public const string EmployeeNotCreator = "Только создатель задания может отменить задание";
        public const string EmployeeNotPerformer = "Только исполнитель задания может приостанавливать, отменять и выполнять задание";
        public const string EmployeeNameTooLong = "Имя работника слишком длинное, ограничение в 100 символов";
        public const string EmployeeLoginTooLong = "Логин работника слишком длинный, ограничение в 100 символов";
        public const string MoneyValueNotCorrect = "Неккоректная сумма награды";
        public const string WorkTaskNotFound = "Задание не было найдено";
        public const string LoginAlreadyExists = "Работник с таким логином уже существует";
        public const string EmployeeNotFound = "Работник не найден";
        public const string CommentTooLong = "Комментарий слишком длинный, ограничение в 300 символов";
        public const string ThemeAlreadyExists = "Тема с таким названием уже существует";
        public const string WorkTaskVersionNotFound = "Версия задания не найдена";
    }
}
