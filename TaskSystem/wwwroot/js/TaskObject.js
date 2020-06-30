/// Класс окна подробностей о задании
let currentTask; /// объект выбранного задания

function TaskObject(id) {
    this.id = id; /// идентификатор задания
    this.commentMessage = null;
    this.commentValidation = "commentValidation";
    this.historyObject = new HistoryObject(this); /// объект окна истории версий задания
    this.statusObject = new StatusObject(this); /// объект окна изменения статуса задания
    this.container = "taskInfoContainer"; /// идентификатор контейнера для подробностей задания
    this.commentSection = "commentSection"; /// идентификатор контейнера для комментариев
    this.infoTemplate = "taskInfoTemplate"; /// название шаблона отрисовки подробностей задания
    this.commentTemplate = "taskCommentsTemplate"; /// название шаблона отрисовки комментариев

    /// Метод получения и отрисовки комментариев выбранного задания
    this.showComments = async function () {
        const data = await CommentRepository.GetCommentsOfTask(this.id);
        switch (data.status) {
            case 0: {
                const html = await Render(this.commentTemplate, data);
                Insert(html, this.commentSection);
                break;
            };
            case 1: {
                Insert(data.errorMessage, this.commentSection);
                break;
            };
            case 2: {
                alert("Сервер не доступен");
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
                break;
            };
            case 1: {
                Insert(data.errorMessage, this.commentValidation);
                break;
            };
            case 2: {
                alert("Сервер не доступен");
                break;
            }
        }
    };
    this.setCurrentComment = function (message) {
        this.commentMessage = message;
    }

    /// Метод получения подробностей о задании и ее отрисовка
    this.render = async function () {
        const data = await TaskRepository.GetLastVersionOfTask(this.id);
        switch (data.status) {
            case 0: {
                const html = await Render(this.infoTemplate, data.result);
                Insert(html, this.container);
                break;
            };
            case 1: {
                Insert(data.errorMessage, this.container);
                break;
            };
            case 2: {
                alert("Сервер не доступен");
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
    await currentTask.showComments();
    currentTask.toggle();
}
