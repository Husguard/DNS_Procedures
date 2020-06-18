-- Шевелев Максим
-- 09.06.2020
-- Получение всех тем
CREATE PROCEDURE [dbo].[TaskProcedureGetAllThemes]
AS
	 SELECT tt.ID, tt.Name FROM TaskTheme AS tt
GO

