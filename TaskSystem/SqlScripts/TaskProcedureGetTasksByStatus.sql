-- Шевелев Максим
-- 08.06.2020
-- Получение последних версий заданий, у которых статус равняется входному значению
CREATE PROCEDURE [dbo].[TaskProcedureGetTasksByStatus]
	@StatusID TINYINT
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
		WHERE StatusID = @StatusID
GO