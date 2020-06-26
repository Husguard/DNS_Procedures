/// Класс окна кнопок смены статуса
function StatusObject(currentTask) {
    this.id = currentTask.id; /// идентификатор задания
    this.newAward = "setMoneyAward"; /// идентификатор окна новой награды

    /// Метод переключения видимости окна новой награды
    this.toggle = function () {
        Toggle(this.newAward);
    };

    /// Метод создания новой версии задания
    /// <moneyAward> - награда за выполнение(новая при взятии в работу задания, иначе старая)
    /// <statusId> - новый статус задания
    this.setStatus = async function (moneyAward, statusId) {
        const data = await TaskRepository.AddTaskVersion(moneyAward, statusId, this.id);
        switch (data.status) {
            case 0: {
                alert("Задание обновлено"); // можно добавить перерисовку задания(getlastversionOfTask)
                break;
            }
            case 1: {
                alert(data.errorMessage);
                break;
            };
            case 2: {
                alert("Сервер не доступен");
                break;
            }
        }
    }
}
