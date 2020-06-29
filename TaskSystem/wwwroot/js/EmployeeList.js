/// Класс таблицы работников
function EmployeeList() {
    this.container = "mainTable"; /// указатель на место отрисовки таблицы работников
    this.template = "employeesTemplate"; /// название шаблона отрисовки таблицы

    /// Метод получения всех работников и проверки результата
    this.getAll = async function () {
        const data = await EmployeeRepository.GetAllEmployees();
        this.checkData(data);
    }

    /// Метод создания HTML верстки на основе данных и ее отрисовка в главной таблице
    /// <json> - JSON объект данных
    this.render = async function (json) {
        const html = await Render(this.template, json);
        Insert(html, "mainTable");
    }

    /// Метод проверки результата - (стоит индивидуально проверять результат или через общий метод??)
    /// <data> - ответ сервера
    this.checkData = function (data) {
        switch (data.status) {
            case 0: {
                this.render(data);
                break;
            }
            case 1: {
                Insert(data.errorMessage, this.container);
                break;
            };
            case 2: {
                alert("Сервер недоступен");
                break;
            }
        }
    }
}
const employeeList = new EmployeeList();