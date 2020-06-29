-- Шевелев Максим
-- 11.06.2020
-- Получение всех версий выбранного задания
CREATE PROCEDURE [dbo].[TaskProcedureGetTaskByID]
	@TaskId INT
AS
	 SELECT 
		ttv.TaskID, 
		tta.Name AS TaskName, 
		tta.Description,
		ttv.Version, 
		ttv.ID AS TaskVersionID, 
		ttv.MoneyAward, 
		ttv.StatusID, 
		ts.Name AS StatusName,
		tta.CreatorID, 
		tec.Name AS CreatorName,
		ttv.PerformerID,
		tep.Name AS PerformerName,
		tta.CreateDate,
		ttv.CreateDate AS CreateVersionDate,
		tta.ExpireDate, 
		tta.ThemeID,
		tth.Name AS ThemeName
	From TaskTaskVersion As ttv
	INNER JOIN TaskTask as tta ON ttv.TaskID = tta.ID
	INNER JOIN TaskStatus as ts ON ttv.StatusID = ts.ID
	INNER JOIN TaskEmployee as tec ON tec.ID = tta.CreatorID
	LEFT JOIN TaskEmployee as tep on tep.ID = ttv.PerformerID
	INNER JOIN TaskTheme as tth on tth.ID = tta.ThemeID
	 WHERE TaskID = @TaskId
	 ORDER BY CreateVersionDate
GO


