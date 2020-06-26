﻿/// Класс окна истории версий задания
function HistoryObject(currentTask) {
    this.id = currentTask.id; /// Идентификатор задания
    this.container = "taskHistoryContainer"; /// Идентификатор контейнера, хранящий историю задания
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
                const html = await Render(this.template, data);
                Insert(html, this.container);
                break;
            };
            case 1: {
                alert(data.errorMessage);
                break;
            };
            case 2: {
                alert("Сервер не доступен");
                break;
            };
        }
        this.toggle();
    };
}