-- Шевелев Максим
-- 09.06.2020
-- Получение всех заданий и их версий
CREATE FUNCTION TaskFunctionGetAllTasksAndLastVersions ()
    RETURNS TABLE
    AS RETURN (
		SELECT 
			ttt.TaskID, 
			ttt.Name AS TaskName,
			ttt.Description,
			ttt.Version, 
			ttt.TaskVersionID,
			ttt.MoneyAward, 
			ttt.StatusID, 
			ttt.StatusName,
			ttt.CreatorID, 
			ttt.CreatorName,
			ttt.PerformerID, 
			ttt.PerformerName,
			ttt.CreateDate,
			ttt.CreateVersionDate AS CreateVersionDate,
			ttt.ExpireDate,
			ttt.ThemeID,
			ttt.ThemeName
		FROM 
		(
			SELECT 
				ttv.TaskID, 
				MAX(ttv.Version) AS LastVersion 
			FROM TaskTaskVersion AS ttv 
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
			GROUP BY TaskID
		) AS LastResult
		INNER JOIN 
			(
				SELECT 
					ttv.ID AS TaskVersionID, 
					ttv.MoneyAward, 
					ttv.Version,
					ttv.StatusID,
					ts.Name AS StatusName,
					ttv.TaskID,
					tta.Name,
					tta.Description,
					tta.CreatorID, 
					tec.Name AS CreatorName,
					ttv.PerformerID, 
					tep.Name AS PerformerName,
					tta.CreateDate, 
					ttv.CreateDate AS CreateVersionDate,
					tta.ExpireDate,
					tta.ThemeID,
					tth.Name AS ThemeName
			FROM TaskTaskVersion AS ttv
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
			INNER JOIN TaskTheme as tth ON tth.ID = tta.ThemeID
			LEFT JOIN TaskEmployee as tep ON tep.ID = ttv.PerformerID
			INNER JOIN TaskEmployee as tec ON tec.ID = tta.CreatorID
			INNER JOIN TaskStatus as ts ON ts.ID = ttv.StatusID
			) AS ttt ON LastResult.TaskID = ttt.TaskID AND LastResult.LastVersion = ttt.Version
		)