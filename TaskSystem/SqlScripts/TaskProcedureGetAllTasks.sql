-- Шевелев Максим
-- 09.06.2020
-- Получение всех заданий и их версий
CREATE PROCEDURE [dbo].[TaskProcedureGetAllTasks]
AS
	 SELECT 
		TaskVersionID,
		MoneyAward,
		Version,
		StatusID
		TaskID,
		CreatorID,
		PerformerID,
		CreateDate,
		ThemeID 
	 FROM TaskFunctionGetAllTasksAndLastVersions()
GO

