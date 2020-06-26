/// Инициализация репозитория комментариев
const CommentRepository = {
    /// Метод получения комментариев задания
    /// <taskId> - идентификатор задания
    GetCommentsOfTask: function (taskId) {
        return FetchGet("GetCommentsOfTask", {
            taskId: taskId
        });
    },
    /// Метод получения комментариев работника
    /// <employeeId> - идентификатор работника
    GetCommentsOfEmployee: function (employeeId) {
        return FetchGet("GetCommentsOfEmployee", {
            employeeId: employeeId
        })
    },
    /// Метод добавления комментария к заданию
    /// <taskId> - идентификатор задания
    /// <message> - текст комментария
    AddCommentToTask: function (taskId, message) {
        return FetchPost("AddCommentToTask", {
            taskId: taskId,
            message: message
        });
    }
}