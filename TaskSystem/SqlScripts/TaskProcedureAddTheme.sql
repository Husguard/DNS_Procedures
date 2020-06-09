-- Шевелев Максим
-- 09.06.2020
-- Добавление новой темы
CREATE PROCEDURE [dbo].[TaskProcedureAddTheme]
	@Name NVARCHAR(100)
AS
	INSERT INTO TaskTheme(Name) 
			VALUES(@Name)
GO

