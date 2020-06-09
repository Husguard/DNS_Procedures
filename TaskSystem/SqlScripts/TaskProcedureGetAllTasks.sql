-- Шевелев Максим
-- 09.06.2020
-- Получение всех заданий и их версий
CREATE PROCEDURE [dbo].[TaskProcedureGetAllTasks]
AS
	 SELECT * FROM TaskFunctionGetAllTasksAndVersions()
GO

