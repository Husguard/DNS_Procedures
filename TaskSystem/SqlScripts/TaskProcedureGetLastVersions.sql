-- Шевелев Максим
-- 09.06.2020
-- Получение всех последних версий заданий
CREATE PROCEDURE [dbo].[TaskProcedureGetLastVersions]
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
