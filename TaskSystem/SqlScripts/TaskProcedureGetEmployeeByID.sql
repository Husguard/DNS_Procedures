-- Шевелев Максим
-- 18.06.2020
-- Получение работника по идентификатору
CREATE PROCEDURE [dbo].[TaskProcedureGetEmployeeByID]
	@EmployeeId INT
AS
	 SELECT 
		 te.ID, te.Name, te.Login
	 FROM TaskEmployee as te
	 WHERE te.ID = @EmployeeId 
GO


