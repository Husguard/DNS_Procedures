/// Класс окна кнопок смены статуса
function StatusObject(currentTask) {
    this.id = currentTask.id; /// идентификатор задания
    this.validation = "taskInfoValidation";
    this.container = document.getElementById("setMoneyAward"); /// идентификатор окна новой награды
    this.moneyValidation = "taskSetMoneyValidation";
    this.infoValidation = "taskInfoValidation";
    this.newAward = 0;

    /// Метод переключения видимости окна новой награды
    this.toggle = function () {
        this.setValidation();
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
                await this.updateTask();
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

    /// Метод обновления указателя на окно ошибки
    this.setValidation = function () {
        if (!this.container.hasAttribute('open')) this.validation = this.moneyValidation;
        else this.validation = this.infoValidation;
    }

    /// Метод обновления окна задания после нового статуса
    this.updateTask = async function () {
        this.container.close();
        this.newAward = 0;
        await currentTask.render();
        await currentTask.showComments();
        this.container = document.getElementById("setMoneyAward"); /// обновление ссылки после рендера
        this.setValidation();
    }
}