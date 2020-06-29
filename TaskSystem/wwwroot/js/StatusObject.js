/// Класс окна кнопок смены статуса
function StatusObject(currentTask) {
    this.id = currentTask.id; /// идентификатор задания
    this.container = "setMoneyAward"; /// идентификатор окна новой награды
    this.newAward = 0;

    /// Метод переключения видимости окна новой награды
    this.toggle = function () {
        Toggle(this.container);
    };
    this.setNewMoneyAward = function (newMoneyAward) {
        this.newAward = newMoneyAward;
    }

    /// Метод создания новой версии задания
    /// <moneyAward> - награда за выполнение(новая при взятии в работу задания, иначе старая)
    /// <statusId> - новый статус задания
    this.setStatus = async function (statusId) {
        const data = await TaskRepository.AddTaskVersion(this.newAward, statusId, this.id);
        switch (data.status) {
            case 0: {
                this.toggle();
                currentTask.render();
                break;
            }
            case 1: {
                Insert(data.errorMessage, "taskInfoValidation");
           //     Insert(data.errorMessage, "taskSetMoneyValidation");
                break;
            };
            case 2: {
                alert("Сервер недоступен");
                break;
            }
        }
    }
}
