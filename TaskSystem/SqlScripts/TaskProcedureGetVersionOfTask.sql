-- Шевелев Максим
-- 09.06.2020
-- Получение определенной версии задания
CREATE PROCEDURE [dbo].[TaskProcedureGetVersionOfTask]
	@TaskID INT,
	@Version TINYINT
AS
	SELECT 
	ttv.ID AS TaskVersionID, 
	ttv.MoneyAward, 
	ttv.Version, 
	ttv.StatusID, 
	ts.Name AS StatusName,
	ttv.TaskID, 
	tta.CreatorID, 
	tec.Name AS CreatorName,
	ttv.PerformerID, 
	tep.Name AS PerformerName,
	tta.CreateDate, 
	tta.ThemeID,
	tth.Name AS ThemeName
		FROM TaskTaskVersion AS ttv 
		INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
		INNER JOIN TaskEmployee as tec ON tec.ID = tta.CreatorID
		LEFT JOIN TaskEmployee as tep on tep.ID = ttv.PerformerID
		INNER JOIN TaskTheme as tth on tth.ID = tta.ThemeID
		INNER JOIN TaskStatus as ts on ts.ID = ttv.StatusID
		WHERE TaskID = @TaskID AND Version = @Version
GO