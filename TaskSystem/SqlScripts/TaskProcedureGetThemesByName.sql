-- Шевелев Максим
-- 11.06.2020
-- Получение тем, которые начинаются с введенной строки
CREATE PROCEDURE [dbo].[TaskProcedureUpdatePerformerOfTask] 
	@Name nvarchar(100)
AS
	 SELECT tt.ID, tt.Name FROM TaskTheme AS tt WHERE tt.Name LIKE @Name + '%'
GO


