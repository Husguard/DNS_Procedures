-- Шевелев Максим
-- 09.06.2020
-- Получение последней версии задания
CREATE PROCEDURE [dbo].[TaskProcedureGetLastVersionOfTask]
	@TaskID INT
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
	WHERE TaskID = @TaskID

