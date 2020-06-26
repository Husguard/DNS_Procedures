/// Инициализация репозитория заданий
const TaskRepository = {
    /// Метод получения последних версий всех заданий
    GetLastVersions: function () {
        return FetchGet("GetLastVersions");
    },

    /// Метод получения истории версий выбранного задания
    /// <taskId> - идентификатор задания
    GetTaskHistory: function (taskId) {
        return FetchGet("GetTaskByID", {
            taskId: taskId
        });
    },

    /// Метод получения заданий, у которых последняя версия имеет выбранный статус
    /// <statusId> - статус задания
    GetTasksByStatus: function (statusId) {
        return FetchGet("GetTasksByStatus", {
            statusId: statusId
        });
    },

    /// Метод получения подробной информации по выбранному заданию
    /// <taskId> - идентификатор задания
    GetLastVersionOfTask: function (taskId) {
        return FetchGet("GetLastVersionOfTask", {
            taskId: taskId
        });
    },

    /// Метод получения заданий, у которых исполнителем был выбранный работник
    /// <performerId> - идентификатор работника
    GetTasksByPerformer: function (performerId) {
        return FetchGet("GetTasksByPerformer", {
            performerId: performerId
        });
    },

    /// Метод получения заданий, у которых создателем был выбранный работник
    /// <creatorId> - идентификатор работника
    GetTasksByCreator: function (creatorId) {
        return FetchGet("GetTasksByCreator", {
            creatorId: creatorId
        });
    },

    /// Метод создания нового задания
    /// <taskName> - название задания
    /// <taskDescription> - описание задания
    /// <taskTheme> - ИДЕНТИФИКАТОР темы
    /// <taskExpireDateTime> - дата и время окончания задания
    AddNewTask: function (taskName, taskDescription, taskTheme, taskExpireDateTime) {
        return FetchPost("AddNewTask", {
            taskName: taskName,
            description: taskDescription,
            themeId: taskTheme,
            expireDate: taskExpireDateTime
        }
        );
    },

    /// Метод создания новой версии задания
    /// <moneyAward> - награда за выполнение, которая меняется при взятии задания на выполнение, иначе остаётся той же
    /// <statusId> - новый статус задания
    /// <taskId> - идентификатор задания
    AddTaskVersion: function (moneyAward, statusId, taskId) {
        return FetchPost("AddTaskVersion", {
            moneyAward: moneyAward,
            statusId: statusId,
            taskId: taskId
        });
    }
}