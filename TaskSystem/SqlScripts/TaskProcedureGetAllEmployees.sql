-- Шевелев Максим
-- 09.06.2020
-- Получение всех работников
CREATE PROCEDURE [dbo].[TaskProcedureGetAllEmployees]
AS
	 SELECT te.ID, te.Name, te.Login
	 FROM TaskEmployee AS te
GO
