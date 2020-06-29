-- Шевелев Максим
-- 09.06.2020
-- Получение заданий, которые выполнял пользователь
CREATE PROCEDURE [dbo].[TaskProcedureGetPerformerTasks]
	@PerformerID INT
AS
	SELECT 
		TaskID, 
		TaskName, 
		Description,
		Version, 
		TaskVersionID, 
		MoneyAward, 
		StatusID, 
		StatusName,
		CreatorID, 
		CreatorName,
		PerformerID,
		PerformerName,
		CreateDate,
		CreateVersionDate,
		ExpireDate, 
		ThemeID,
		ThemeName
	FROM TaskFunctionGetAllTasksAndLastVersions()
		WHERE PerformerID = @PerformerID
GO

