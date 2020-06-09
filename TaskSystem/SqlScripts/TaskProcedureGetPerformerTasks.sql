-- Шевелев Максим
-- 09.06.2020
-- Получение заданий, которые выполнял пользователь
CREATE PROCEDURE [dbo].[TaskProcedureGetPerformerTasks]
	@PerformerID INT
AS
	SELECT TaskVersionID, 
	 MoneyAward, 
	 Version, 
	 StatusID,
	 TaskID,
	 CreatorID,
	 PerformerID,
	 CreateDate,
	 ThemeID FROM TaskFunctionGetAllTasksAndVersions() 
		WHERE PerformerID = @PerformerID
GO

