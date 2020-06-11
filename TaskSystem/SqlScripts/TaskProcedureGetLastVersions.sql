-- Шевелев Максим
-- 09.06.2020
-- Получение всех последних версий заданий
SELECT TaskID, Version, TaskVersionID, MoneyAward, StatusID, CreatorID, PerformerID, CreateDate, ThemeID FROM 
		TaskFunctionGetAllTasksAndVersions()
