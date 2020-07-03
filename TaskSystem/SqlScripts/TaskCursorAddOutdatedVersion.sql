-- Шевелев Максим
-- 03.07.2020
-- Создание расписания, в котором запускается выполнение задания, добавляющее версии для устаревших заданий
USE [TaskManage]
DECLARE  @TaskId    INT
DECLARE TaskCursorOutdatedTasks CURSOR LOCAL FOR
	SELECT 
		ttt.TaskID
		FROM 
		(
			SELECT 
				ttv.TaskID, 
				MAX(ttv.Version) AS LastVersion 
			FROM TaskTaskVersion AS ttv 
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
			WHERE ExpireDate < GETDATE()
			GROUP BY TaskID
		) AS LastResult
		INNER JOIN 
			(
			SELECT 
				ttv.Version,
				ttv.TaskID,
				tta.ExpireDate
			FROM TaskTaskVersion AS ttv
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
			WHERE ttv.StatusID != 5
			) AS ttt ON LastResult.TaskID = ttt.TaskID AND LastResult.LastVersion = ttt.Version
OPEN TaskCursorOutdatedTasks
FETCH NEXT FROM TaskCursorOutdatedTasks INTO @TaskId
WHILE @@FETCH_STATUS=0
BEGIN
	EXEC TaskProcedureAddTaskVersion 0,5,@TaskId,NULL
    FETCH NEXT FROM TaskCursorOutdatedTasks 
      INTO  @TaskId
END
CLOSE TaskCursorOutdatedTasks
DEALLOCATE TaskCursorOutdatedTasks