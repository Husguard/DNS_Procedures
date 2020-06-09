-- Шевелев Максим
-- 09.06.2020
-- Получение всех заданий и их версий
CREATE FUNCTION TaskFunctionGetAllTasksAndVersions ()
    RETURNS TABLE
    AS RETURN (SELECT ttv.ID AS TaskVersionID, ttv.MoneyAward, ttv.Version, 
	ttv.StatusID,
	ttv.TaskID,
	tta.CreatorID,
	ttv.PerformerID,
	tta.CreateDate,
	tta.ThemeID
		FROM TaskTaskVersion AS ttv 
		INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID)