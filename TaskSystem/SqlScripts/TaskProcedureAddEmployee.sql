-- Шевелев Максим
-- 09.06.2020
-- Добавление нового работника с использованием имени и уникального логина
CREATE PROCEDURE [dbo].[TaskProcedureAddEmployee]
	@Name NVARCHAR(100),
	@Login NVARCHAR(100)
AS
	INSERT INTO TaskEmployee(Name, Login) 
			VALUES(@Name, @Login)
GO

