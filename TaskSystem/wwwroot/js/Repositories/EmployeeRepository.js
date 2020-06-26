/// Инициализация репозитория работников
const EmployeeRepository = {
    /// Метод получения всех зарегистрированных работников
    GetAllEmployees: function () {
        return FetchGet("GetAllEmployees");
    },

    /// Метод получения работника, у которого введенный логин
    /// <login> - логин работника
    GetEmployeeByLogin: function (login) {
        return FetchGet("GetEmployeeByLogin", {
            login: login
        })
    },

    /// Метод создания нового работника
    /// <name> - имя работника
    /// <login> - логин работника
    RegisterNewEmployee: function (name, login) {
        return FetchPost("RegisterNewEmployee", {
            employeeName: name,
            login: login
        });
    }
}



