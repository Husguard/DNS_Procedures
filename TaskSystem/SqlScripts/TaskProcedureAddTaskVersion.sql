-- Шевелев Максим
-- 09.06.2020
-- Добавление новой версии задания
CREATE PROCEDURE [dbo].[TaskProcedureAddTaskVersion] 
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

