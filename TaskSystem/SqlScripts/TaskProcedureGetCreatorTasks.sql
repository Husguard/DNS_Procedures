-- Шевелев Максим
-- 09.06.2020
-- Получение заданий, которые создал пользователь
CREATE PROCEDURE [dbo].[TaskProcedureGetCreatorTasks]
	@CreatorID INT
AS
	SELECT TaskVersionID, MoneyAward, Version, StatusID, TaskID, CreatorID, PerformerID, CreateDate, ThemeID FROM TaskFunctionGetAllTasksAndVersions()
		WHERE CreatorID = @CreatorID
GO

