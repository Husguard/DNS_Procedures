-- Шевелев Максим
-- 09.06.2020
-- Получение определенной версии задания
CREATE PROCEDURE [dbo].[TaskProcedureGetVersionOfTask]
	@TaskID INT,
	@Version TINYINT
AS
	SELECT TaskVersionID, MoneyAward, Version, StatusID, TaskID, CreatorID, PerformerID, CreateDate, ThemeID FROM TaskFunctionGetAllTasksAndVersions()
		WHERE TaskID = @TaskID AND Version = @Version
GO

