-- Шевелев Максим
-- 09.06.2020
-- Получение всех заданий и их версий
CREATE FUNCTION TaskFunctionGetAllTasksAndLastVersions ()
    RETURNS TABLE
    AS RETURN (SELECT ttt.TaskID, Version, TaskVersionID, MoneyAward, StatusID, CreatorID, PerformerID, CreateDate, ThemeID FROM 
		(SELECT ttv.TaskID, MAX(ttv.Version) AS LastVersion 
		FROM TaskTaskVersion AS ttv 
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
				GROUP BY TaskID) AS LastResult
		 INNER JOIN (SELECT ttv.ID AS TaskVersionID, ttv.MoneyAward, ttv.Version, ttv.StatusID, ttv.TaskID,tta.CreatorID, ttv.PerformerID, tta.CreateDate, tta.ThemeID
				FROM TaskTaskVersion AS ttv 
					INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID) AS ttt ON LastResult.TaskID = ttt.TaskID AND LastResult.LastVersion = ttt.Version)