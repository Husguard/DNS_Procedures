-- Шевелев Максим
-- 09.06.2020
-- Получение всех комментариев от определенного работника
CREATE PROCEDURE [dbo].[TaskProcedureGetCommentsOfEmployee]
	@EmployeeID INT
AS
	 SELECT * FROM TaskComment 
		INNER JOIN TaskEmployee ON (TaskEmployee.ID = TaskComment.EmployeeID AND TaskComment.EmployeeID = @EmployeeID)
GO

