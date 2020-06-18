-- Шевелев Максим
-- 09.06.2020
-- Добавление нового комментария к заданию
CREATE PROCEDURE [dbo].[TaskProcedureAddComment]
	@TaskID INT,
	@EmployeeID INT,
	@Message NVARCHAR(300)
AS
	INSERT INTO TaskComment(Message, TaskID, EmployeeID) 
		VALUES(@Message, @TaskID, @EmployeeID)
GO

