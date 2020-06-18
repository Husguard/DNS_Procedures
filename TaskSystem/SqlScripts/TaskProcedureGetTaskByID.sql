-- Шевелев Максим
-- 11.06.2020
-- Получение тем, которые начинаются с введенной строки
CREATE PROCEDURE [dbo].[TaskProcedureGetEmployeeByID]
	@EmployeeId INT
AS
	 SELECT 
		 te.ID, 
		 te.Name, 
		 te.Login
	 FROM TaskEmployee as te
	 WHERE te.ID = @EmployeeId 
GO


