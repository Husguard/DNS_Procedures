-- Шевелев Максим
-- 09.06.2020
-- Получение заданий, которые выполнял пользователь
CREATE PROCEDURE [dbo].[TaskProcedureGetPerformerTasks]
	@PerformerID INT
AS
	SELECT * FROM TaskFunctionGetAllTasksAndVersions() 
		WHERE PerformerID = @PerformerID
GO

