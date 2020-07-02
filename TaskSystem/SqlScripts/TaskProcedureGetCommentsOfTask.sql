-- Шевелев Максим
-- 09.06.2020
-- Получение всех комментариев к заданию
CREATE PROCEDURE [dbo].[TaskProcedureGetCommentsOfTask]
	@TaskID INT
AS
	 SELECT 
		tc.EmployeeID,
		te.Name, 
		tc.TaskID,
		tc.Message,
		tc.CreateDate
	FROM TaskComment as tc 
	INNER JOIN TaskEmployee as te ON te.ID = tc.EmployeeID
	WHERE tc.TaskID = @TaskID
	ORDER BY tc.CreateDate
GO

