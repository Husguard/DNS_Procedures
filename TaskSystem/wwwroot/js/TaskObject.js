/// Класс окна подробностей о задании
function TaskObject(id) {
    this.id = id; /// идентификатор задания
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
                const html = Render(this.commentTemplate, data);
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
    this.writeComment = async function (message) {
        const data = await CommentRepository.AddCommentToTask(this.id, message);
        switch (data.status) {
            case 0: {
                this.showComments();
                break;
            };
            case 1: {
                alert(data.errorMessage);
                break;
            };
            case 2: {
                alert("Сервер не доступен");
                break;
            }
        }
    };

    /// Метод получения подробностей о задании и ее отрисовка
    this.render = async function () {
        const data = await TaskRepository.GetLastVersionOfTask(this.id);
        switch (data.status) {
            case 0: {
                const html = await Render(this.infoTemplate, data);
                Insert(html, this.container);
            };
            case 1: {
                alert(data.errorMessage);
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
let currentTask; /// объект выбранного задания

/// Метод создания и инициализации объекта выбранного задания
/// <id> - идентификатор задания
function setCurrentTask(id) {
    currentTask = new TaskObject(id);
    currentTask.render();
    currentTask.showComments();
    currentTask.toggle();
}
