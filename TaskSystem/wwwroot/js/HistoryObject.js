/// Класс окна истории версий задания
function HistoryObject(currentTask) {
    this.id = currentTask.id; /// Идентификатор задания
    this.container = document.getElementById("taskHistoryContainer"); /// Идентификатор контейнера, хранящий историю задания
    this.template = "taskHistoryTemplate"; /// Название шаблона для истории задания

    /// Метод переключения видимости окна истории задания
    this.toggle = function () {
        Toggle(this.container);
    };

    /// Метод получения истории текущего выбранного задания
    this.showHistory = async function () {
        const data = await TaskRepository.GetTaskHistory(this.id);
        switch (data.status) {
            case 0: {
                const html = await Render(this.template, data.result);
                this.container = document.getElementById("taskHistoryContainer"); /// Идентификатор контейнера, хранящий историю задания
                this.container.innerHTML = html;
                break;
            };
            case 1: {
                this.container.innerHTML = data.errorMessage;
                break;
            };
            case 2: {
                alert("Возникла критическая ошибка");
                break;
            };
        }
        this.toggle();
    };
}