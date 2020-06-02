CREATE PROCEDURE [ProcedureAddEmployee]
	@Name NVARCHAR(50)
AS
	 INSERT INTO Employee(Name) VALUES(@Name)
GO
CREATE PROCEDURE [ProcedureAddTheme]
	@Name NVARCHAR(50)
AS
	 INSERT INTO Theme(Name) VALUES(@Name)
GO
CREATE PROCEDURE [ProcedureAddComment]
	@TaskID INT,
	@EmployeeID INT,
	@Message NVARCHAR(50)
AS
	 INSERT INTO Comment(Message, TaskID, EmployeeID) VALUES(@Message, @TaskID, @EmployeeID)
GO
CREATE PROCEDURE [ProcedureAddTask]
	@Name NVARCHAR(50),
	@Description NVARCHAR(50),
	@ThemeID INT,
	@CreatorID INT,
	@CreateDate DATE,
	@ExpireDate DATE
AS
	 INSERT INTO Task(Name, Description, ThemeID, CreatorID, CreateDate, ExpireDate) VALUES(@Name, @Description, @ThemeID, @CreatorID, @CreateDate, @ExpireDate)
	 EXEC ProcedureAddTaskVersion NULL, 1, SCOPE_IDENTITY, NULL
GO
CREATE PROCEDURE [ProcedureAddTaskVersion] 
	@MoneyAward MONEY,
	@StatusID TINYINT,
	@TaskID INT,
	@PerformerID INT
AS
	DECLARE @Version int;
	SET @Version = SELECT (COUNT(*)) FROM TaskVersion WHERE TaskVersion.TaskID = @TaskID
	 INSERT INTO TaskVersion(MoneyAward, StatusID, TaskID, PerformerID, Version) VALUES(@MoneyAward, @StatusID, @TaskID, @PerformerID, @Version)
	 /*старая версия отменяется? актуальна только выполненая или в работе*/
GO

CREATE PROCEDURE [ProcedureGetCommentsOfTask]
	@TaskID INT
AS
	 SELECT * FROM Comment WHERE TaskID = @TaskID
GO
CREATE PROCEDURE [ProcedureGetLastTaskVersion] /*проверить*/
	@TaskID INT
AS
	SELECT * FROM (SELECT * FROM TaskVersion INNER JOIN Task ON Task.ID = TaskVersion.TaskID WHERE TaskVersion.TaskID = @TaskID) AS AllVersions HAVING AllVersions.Version = MAX(AllVersions.Version)
GO
CREATE PROCEDURE [ProcedureGetTasksByStatus]
	@StatusID TINYINT
AS
	 SELECT * FROM Task WHERE StatusID = @StatusID
GO
CREATE PROCEDURE [ProcedureUpdateStatusOfTask] /*бэк делает выбор статуса*/
	@TaskID INT,
	@StatusID TINYINT
AS
	 UPDATE TaskVersion SET StatusID = @StatusID WHERE TaskID = @TaskID
GO
CREATE PROCEDURE [ProcedureUpdatePerformerOfTask]
	@EmployeeID INT,
	@TaskID INT
AS
	 UPDATE TaskVersion SET PerformerID = @EmployeeID WHERE TaskID = @TaskID
	 EXEC ProcedureUpdateStatusOfTask @TaskID, 2
GO
