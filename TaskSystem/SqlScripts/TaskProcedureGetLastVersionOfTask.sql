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
	WHERE TaskID = @TaskID

