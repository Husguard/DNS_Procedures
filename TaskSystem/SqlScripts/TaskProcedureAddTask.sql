-- Шевелев Максим
-- 09.06.2020
-- Добавление нового задания и создание нулевой версии
CREATE PROCEDURE [dbo].[TaskProcedureAddTask]
	@Name NVARCHAR(100),
	@Description NVARCHAR(500),
	@ThemeID INT,
	@CreatorID INT,
	@ExpireDate DATE
AS
	INSERT INTO TaskTask(Name, Description, ThemeID, CreatorID, CreateDate, ExpireDate) 
			VALUES(@Name, @Description, @ThemeID, @CreatorID, GETDATE(), @ExpireDate)
	 EXEC TaskProcedureAddTaskVersion NULL, 1, Scope_identity, NULL
GO

