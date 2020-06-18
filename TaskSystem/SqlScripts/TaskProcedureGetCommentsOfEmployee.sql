-- Шевелев Максим
-- 09.06.2020
-- Получение всех комментариев от определенного работника
CREATE PROCEDURE [dbo].[TaskProcedureGetCommentsOfEmployee]
	@EmployeeID INT
AS
	 SELECT tc.EmployeeID, te.Login, te.Name, tc.TaskID, tc.Message FROM TaskComment AS tc 
		INNER JOIN TaskEmployee AS te ON (te.ID = tc.EmployeeID AND tc.EmployeeID = @EmployeeID)
GO

