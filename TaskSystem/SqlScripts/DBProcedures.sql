use intership
GO
/*Получение всех заданий и их версий*/
CREATE FUNCTION TaskFunctionGetAllTasksAndVersions ()
    RETURNS TABLE
    AS RETURN (SELECT ttv.ID AS TaskVersionID, ttv.MoneyAward, ttv.Version, 
	ttv.StatusID, ts.Name AS StatusName, 
	ttv.TaskID, tta.Name AS TaskName, tta.Description,
	tta.CreatorID, tec.Name AS Creator,
	ttv.PerformerID, tep.Name AS Performer,
	tta.CreateDate, tta.ExpireDate,
	tta.ThemeID, tth.Name AS ThemeName
		FROM TaskTaskVersion AS ttv 
		INNER JOIN TaskStatus AS ts ON ts.ID = ttv.StatusID 
		INNER JOIN TaskTask AS tta ON ttv.TaskID = tta.ID
		INNER JOIN TaskEmployee AS tec ON tta.CreatorID = tec.ID
		LEFT OUTER JOIN TaskEmployee AS tep ON ttv.PerformerID = tep.ID OR ttv.PerformerID = NULL
		INNER JOIN TaskTheme AS tth ON tta.ThemeID = tth.ID)
GO
/*Добавление работника */
CREATE PROCEDURE [TaskProcedureAddEmployee]
	@Name NVARCHAR(100)
AS
	INSERT INTO TaskEmployee(Name) 
			VALUES(@Name)
GO
/*Добавление темы */
CREATE PROCEDURE [TaskProcedureAddTheme]
	@Name NVARCHAR(100)
AS
	INSERT INTO TaskTheme(Name) 
			VALUES(@Name)
GO
/*Добавление комментария */
CREATE PROCEDURE [TaskProcedureAddComment]
	@TaskID INT,
	@EmployeeID INT,
	@Message NVARCHAR(300)
AS
	INSERT INTO TaskComment(Message, TaskID, EmployeeID) 
		VALUES(@Message, @TaskID, @EmployeeID)
GO
/*Добавление новой версии задания */
CREATE PROCEDURE [TaskProcedureAddTaskVersion] 
	@MoneyAward MONEY,
	@StatusID TINYINT,
	@TaskID INT,
	@PerformerID INT
AS
	DECLARE @Version int;
	SET @Version = (SELECT COUNT(*) AS Count FROM TaskTaskVersion WHERE TaskTaskVersion.TaskID = @TaskID)
	INSERT INTO TaskTaskVersion(MoneyAward, StatusID, TaskID, PerformerID, Version) 
			VALUES(@MoneyAward, @StatusID, @TaskID, @PerformerID, @Version)
GO
/*Добавление нового задания*/
CREATE PROCEDURE [TaskProcedureAddTask]
	@Name NVARCHAR(100),
	@Description NVARCHAR(500),
	@ThemeID INT,
	@CreatorID INT,
	@ExpireDate DATE
AS
	INSERT INTO TaskTask(Name, Description, ThemeID, CreatorID, CreateDate, ExpireDate) 
			VALUES(@Name, @Description, @ThemeID, @CreatorID, GETDATE(), @ExpireDate)
	 EXEC TaskProcedureAddTaskVersion NULL, 1, @@IDENTITY, NULL
GO
/*Получение всех комментариев к заданию */
CREATE PROCEDURE [TaskProcedureGetCommentsOfTask]
	@TaskID INT
AS
	 SELECT * FROM TaskComment 
		INNER JOIN TaskEmployee ON (TaskEmployee.ID = TaskComment.EmployeeID AND TaskComment.TaskID = @TaskID)
GO
/* Получение определенной версии задания */
CREATE PROCEDURE [TaskProcedureGetVersionOfTask]
	@TaskID INT,
	@Version TINYINT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions() 
		WHERE TaskVersionID = @TaskID AND Version = @Version
GO
/*Получение всех заданий и их версий*/
CREATE PROCEDURE [TaskProcedureGetAllTasks]
AS
	 SELECT * FROM TaskFunctionGetAllTasksAndVersions()
GO
/*Получение всех заданий по статусу*/
CREATE PROCEDURE [TaskProcedureGetTasksByStatus]
	@StatusID TINYINT
AS
	 SELECT * FROM TaskFunctionGetAllTasksAndVersions() 
		WHERE StatusID = @StatusID
GO
/*Изменить статус задания */
CREATE PROCEDURE [TaskProcedureUpdateStatusOfTask]
	@TaskID INT,
	@Version TINYINT,
	@StatusID TINYINT
AS
	 UPDATE TaskTaskVersion SET StatusID = @StatusID 
		WHERE TaskTaskVersion.TaskID = @TaskID AND TaskTaskVersion.Version = @Version
GO
/*Получение заданий, которые выполняет пользователь*/
CREATE PROCEDURE [TaskProcedureGetPerformerTasks]
	@PerformerID INT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions() 
		WHERE PerformerID = @PerformerID
GO
/*Получение заданий, которые создал пользователь */
CREATE PROCEDURE [TaskProcedureGetCreatorTasks]
	@CreatorID INT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions()
		WHERE CreatorID = @CreatorID
GO
/*Изменить выполняющего у версии задания*/
CREATE PROCEDURE [TaskProcedureUpdatePerformerOfTask] 
	@TaskID INT,
	@Version TINYINT,
	@EmployeeID INT
AS
	 UPDATE TaskTaskVersion SET PerformerID = @EmployeeID
		WHERE TaskTaskVersion.TaskID = @TaskID AND TaskTaskVersion.Version = @Version
GO
/*Получение последней версии задания*/
CREATE PROCEDURE [TaskProcedureGetLastVersionOfTask]
	@TaskID INT
AS
	SELECT * FROM 
		(SELECT TaskID, MAX(Version) AS LastVersion FROM TaskFunctionGetAllTasksAndVersions() 
			WHERE TaskID = @TaskID GROUP BY TaskID) AS LastResult
			INNER JOIN TaskFunctionGetAllTasksAndVersions() ON LastVersion = LastResult.LastVersion
GO