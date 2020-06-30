-- Шевелев Максим
-- 09.06.2020
-- Получение всех работников
CREATE PROCEDURE [dbo].[TaskProcedureGetEmployeeByLogin]
	@Login nvarchar(100)
AS
	 SELECT 
		te.ID,
		te.Name, 
		te.Login
	 FROM TaskEmployee AS te 
	 WHERE te.Login LIKE @Login
GO
