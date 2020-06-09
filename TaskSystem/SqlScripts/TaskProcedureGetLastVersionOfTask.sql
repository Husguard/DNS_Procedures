-- Шевелев Максим
-- 09.06.2020
-- Получение последней версии задания
CREATE PROCEDURE [dbo].[TaskProcedureGetLastVersionOfTask]
	@TaskID INT
AS
	SELECT * FROM 
		(SELECT ttv.TaskID, MAX(ttv.Version) AS LastVersion 
		FROM TaskTaskVersion AS ttv 
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
				WHERE TaskID = @TaskID GROUP BY TaskID) AS LastResult
		INNER JOIN TaskTask AS tta ON tta.ID = LastResult.TaskID
GO

