/// Класс окна кнопок смены статуса
function StatusObject(currentTask) {
    this.id = currentTask.id; /// идентификатор задания
    this.validation = "taskInfoValidation";
    this.container = document.getElementById("setMoneyAward"); /// идентификатор окна новой награды
    this.newAward = 0;

    /// Метод переключения видимости окна новой награды
    this.toggle = function () {
        this.validation = (this.validation == "taskSetMoneyValidation") ? "taskInfoValidation" : "taskSetMoneyValidation";
        // необходимо обновлять значение moneyAward при смене окна
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
                this.container.close();
                this.validation = (this.validation == "taskSetMoneyValidation") ? "taskInfoValidation" : "taskSetMoneyValidation";
                await currentTask.render();
                await currentTask.showComments();
                break;
            }
            case 1: {
                document.getElementById(this.validation).innerHTML = data.errorMessage;
                break;
            };
            case 2: {
                alert("Возникла критическая ошибка");
                break;
            }
        }
    }
}