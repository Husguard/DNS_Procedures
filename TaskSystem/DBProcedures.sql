CREATE FUNCTION TaskFunctionGetAllTasksAndVersions ()
    RETURNS TABLE
    AS RETURN (SELECT TaskTaskVersion.ID, TaskTaskVersion.MoneyAward, TaskTaskVersion.Version, 
	TaskTaskVersion.StatusID, TaskStatus.Name AS StatusName, 
	TaskTaskVersion.TaskID, TaskTask.Name AS TaskName, TaskTask.Description,
	TaskTask.CreatorID, TaskEmployee.Name AS Creator,
	TaskTaskVersion.PerformerID, TaskEmployee.Name AS Performer,
	TaskTask.CreateDate, TaskTask.ExpireDate,
	TaskTask.ThemeID, TaskTheme.Name AS ThemeName
		FROM TaskTaskVersion 
		INNER JOIN TaskStatus ON TaskStatus.ID = TaskTaskVersion.StatusID 
		INNER JOIN TaskEmployee ON TaskTaskVersion.PerformerID = TaskEmployee.ID 
		INNER JOIN TaskTask ON TaskTaskVersion.TaskID = TaskTask.ID
		INNER JOIN TaskTheme ON TaskTask.ThemeID = TaskTheme.ID
		WHERE TaskEmployee.ID = TaskTask.CreatorID AND TaskTaskVersion.TaskID = TaskTask.ID)
GO
CREATE PROCEDURE [TaskProcedureAddEmployee]
	@Name NVARCHAR(100)
AS
	 INSERT INTO TaskEmployee(Name) VALUES(@Name)
GO
CREATE PROCEDURE [TaskProcedureAddTheme]
	@Name NVARCHAR(100)
AS
	 INSERT INTO TaskTheme(Name) VALUES(@Name)
GO
CREATE PROCEDURE [TaskProcedureAddComment]
	@TaskID INT,
	@EmployeeID INT,
	@Message NVARCHAR(300)
AS
	 INSERT INTO TaskComment(Message, TaskID, EmployeeID) VALUES(@Message, @TaskID, @EmployeeID)
GO
CREATE PROCEDURE [TaskProcedureAddTask]
	@Name NVARCHAR(100),
	@Description NVARCHAR(500),
	@ThemeID INT,
	@CreatorID INT,
	@ExpireDate DATE
AS
	 INSERT INTO TaskTask(Name, Description, ThemeID, CreatorID, CreateDate, ExpireDate) VALUES(@Name, @Description, @ThemeID, @CreatorID, GETDATE(), @ExpireDate)
	 EXEC ProcedureAddTaskVersion NULL, 1, SCOPE_IDENTITY, NULL
GO
CREATE PROCEDURE [TaskProcedureAddTaskVersion] 
	@MoneyAward MONEY,
	@StatusID TINYINT,
	@TaskID INT,
	@PerformerID INT
AS
	DECLARE @Version int;
	SET @Version = (SELECT COUNT(*) AS Count FROM TaskTaskVersion WHERE TaskTaskVersion.TaskID = @TaskID)
	 INSERT INTO TaskTaskVersion(MoneyAward, StatusID, TaskID, PerformerID, Version) VALUES(@MoneyAward, @StatusID, @TaskID, @PerformerID, @Version)
	 /*старая версия отменяется? актуальна только выполненая или в работе*/
GO
CREATE PROCEDURE [TaskProcedureGetCommentsOfTask]
	@TaskID INT
AS
	 SELECT * FROM TaskComment INNER JOIN TaskEmployee ON (TaskEmployee.ID = TaskComment.EmployeeID AND TaskComment.TaskID = @TaskID)
GO
CREATE PROCEDURE [TaskProcedureGetVersionOfTask]
	@TaskID INT,
	@Version TINYINT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions() WHERE TaskTask.ID = @TaskID AND TaskTaskVersion.Version = @Version
GO
CREATE PROCEDURE [TaskProcedureGetAllTasks]
AS
	 SELECT * FROM TaskFunctionGetAllTasksAndVersions()
GO
CREATE PROCEDURE [TaskProcedureGetTasksByStatus]
	@StatusID TINYINT
AS
	 SELECT * FROM TaskFunctionGetAllTasksAndVersions() WHERE TaskTaskVersion.StatusID = @StatusID
GO
CREATE PROCEDURE [TaskProcedureUpdateStatusOfTask]
	@TaskID INT,
	@Version TINYINT,
	@StatusID TINYINT
AS
	 UPDATE TaskTaskVersion SET StatusID = @StatusID WHERE TaskTaskVersion.TaskID = @TaskID AND TaskTaskVersion.Version = @Version
GO
CREATE PROCEDURE [TaskProcedureGetPerformerTasks]
	@PerformerID INT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions() WHERE TaskTaskVersion.PerformerID = @PerformerID
GO
CREATE PROCEDURE [TaskProcedureGetCreatorTasks]
	@CreatorID INT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions()  WHERE TaskTask.CreatorID = @CreatorID
GO
/*сомнительные*/
CREATE PROCEDURE [TaskProcedureUpdatePerformerOfTask] 
	@TaskID INT,
	@Version TINYINT,
	@EmployeeID INT
AS
	 UPDATE TaskTaskVersion SET PerformerID = @EmployeeID WHERE TaskTaskVersion.TaskID = @TaskID AND TaskTaskVersion.Version = @Version
GO
CREATE PROCEDURE [TaskProcedureGetLastVersionOfTask]
	@TaskID INT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions() WHERE TaskTask.ID = @TaskID HAVING TaskTaskVersion.Version = MAX(TaskTaskVersion.Version)
GO