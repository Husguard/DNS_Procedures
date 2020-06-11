-- Шевелев Максим
-- 09.06.2020
-- Получение определенной версии задания
CREATE PROCEDURE [dbo].[TaskProcedureGetVersionOfTask]
	@TaskID INT,
	@Version TINYINT
AS
	SELECT ttv.ID AS TaskVersionID, ttv.MoneyAward, ttv.Version, ttv.StatusID, ttv.TaskID, tta.CreatorID, ttv.PerformerID, tta.CreateDate, tta.ThemeID
		FROM TaskTaskVersion AS ttv INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
		WHERE TaskID = @TaskID AND Version = @Version
GO

