-- Шевелев Максим
-- 09.06.2020
-- Получение заданий, которые создал пользователь
CREATE PROCEDURE [dbo].[TaskProcedureGetCreatorTasks]
	@CreatorID INT
AS
	SELECT 
		TaskID, 
		TaskName, 
		Description,
		Version, 
		TaskVersionID, 
		MoneyAward, 
		StatusID, 
		CreatorID, 
		PerformerID,
		CreateDate,
		CreateVersionDate,
		ExpireDate, 
		ThemeID 
	FROM TaskFunctionGetAllTasksAndLastVersions()
		WHERE CreatorID = @CreatorID

GO

