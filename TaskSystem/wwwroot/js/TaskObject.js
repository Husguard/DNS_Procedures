/// Класс окна подробностей о задании
let currentTask; /// объект выбранного задания

function TaskObject(id) {
    this.id = id; /// идентификатор задания
    this.commentMessage = null; /// введенный комментарий
    this.commentValidation = document.getElementById("commentValidation"); /// указатель на элемент вывода ошибки для комментария
    this.historyObject = new HistoryObject(this); /// объект окна истории версий задания
    this.container = document.getElementById("taskInfoContainer"); /// идентификатор контейнера для подробностей задания
    this.commentSection = document.getElementById("commentSection"); /// идентификатор контейнера для комментариев
    this.infoTemplate = "taskInfoTemplate"; /// название шаблона отрисовки подробностей задания
    this.commentTemplate = "taskCommentsTemplate"; /// название шаблона отрисовки комментариев

    /// Метод получения и отрисовки комментариев выбранного задания
    this.showComments = async function () {
        const data = await CommentRepository.GetCommentsOfTask(this.id);
        switch (data.status) {
            case 0: {
                const html = await Render(this.commentTemplate, data);
                this.commentSection.innerHTML = html;
                this.commentValidation = document.getElementById("commentValidation");
                break;
            };
            case 1: {
                this.commentSection.innerHTML = data.errorMessage;
                break;
            };
            case 2: {
                alert("Возникла критическая ошибка");
                break;
            }
        }
    };
    /// Метод добавления нового комментария
    /// <message> - текст комментария 
    this.saveComment = async function () {
        const data = await CommentRepository.AddCommentToTask(this.id, this.commentMessage);
        switch (data.status) {
            case 0: {
                this.showComments();
                this.commentMessage = null;
                break;
            };
            case 1: {
                this.commentValidation.innerHTML = data.errorMessage;
                break;
            };
            case 2: {
                alert("Возникла критическая ошибка");
                break;
            }
        }
    };

    /// Метод установки текущего введенного комментария
    this.setCurrentComment = function (message) {
        this.commentMessage = message;
    }

    /// Метод получения подробностей о задании и ее отрисовка
    this.render = async function () {
        const data = await TaskRepository.GetLastVersionOfTask(this.id);
        switch (data.status) {
            case 0: {
                const html = await Render(this.infoTemplate, data.result);
                this.container.innerHTML = html;
                this.commentSection = document.getElementById("commentSection"); /// идентификатор контейнера для комментариев
                this.container = document.getElementById("taskInfoContainer"); /// идентификатор контейнера для подробностей задания
                break;
            };
            case 1: {
                this.container.innerHTML = data.errorMessage;
                break;
            };
            case 2: {
                alert("Возникла критическая ошибка");
                break;
            }
        }
    };

    /// Метод переключения видимости окна подробностей задания
    this.toggle = function () {
        Toggle(this.container);
    }
}

/// Метод создания и инициализации объекта выбранного задания
/// <id> - идентификатор задания
async function setCurrentTask(id) {
    currentTask = new TaskObject(id);
    await currentTask.render();
    currentTask.statusObject = new StatusObject(currentTask); /// объект окна изменения статуса задания
    await currentTask.showComments();
    currentTask.toggle();
}
