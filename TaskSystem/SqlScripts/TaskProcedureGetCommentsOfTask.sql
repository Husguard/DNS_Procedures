-- Шевелев Максим
-- 09.06.2020
-- Получение всех комментариев к заданию
CREATE PROCEDURE [dbo].[TaskProcedureGetCommentsOfTask]
	@TaskID INT
AS
	 SELECT * FROM TaskComment 
		INNER JOIN TaskEmployee ON (TaskEmployee.ID = TaskComment.EmployeeID AND TaskComment.TaskID = @TaskID)
GO

