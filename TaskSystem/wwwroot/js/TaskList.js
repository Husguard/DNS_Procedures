/// Класс таблицы списка заданий
function TaskList() {
    this.container = "mainTable"; /// указатель на место отрисовки таблицы заданий
    this.template = "tasksTemplate"; /// название шаблона отрисовки таблицы заданий
    /// Метод получения последних версий всех заданий и их отрисовка
    this.getAll = async function () {
        const data = await TaskRepository.GetLastVersions();
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
                alert("Сервер не доступен");
                break;
            }
        }
    }
    /// метод получения последних версий заданий, у которых выбранный статус
    /// <statusId> - статус задания
    this.getByStatus = async function (statusId) {
        const data = await TaskRepository.GetTasksByStatus(statusId);
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
                alert("Сервер не доступен");
                break;
            }
        }
    }

    /// метод получения последних версий заданий, у которых выбранный исполнитель
    /// <employeeId> - идентификатор работника
    this.getByEmployee = async function (employeeId) {
        const data = await TaskRepository.GetTasksByPerformer(employeeId);
        this.checkData(data);
    }

    /// метод проверки статуса запроса, на основе которого идет отрисовка
    /// <data> - результат запроса
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
                alert("Сервер не доступен");
                break;
            }
        }
    }

    /// Метод отрисовки HTML верстки
    /// <json> - JSON объект данных
    this.render = function (json) {
        const html = await Render(this.template, json);
        Insert(html, this.container);
    }
}
const taskList = new TaskList();