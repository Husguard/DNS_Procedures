-- Шевелев Максим
-- 09.06.2020
-- Добавление нового задания и создание нулевой версии
CREATE PROCEDURE [dbo].[TaskProcedureAddTask]
	@Name NVARCHAR(100),
	@Description NVARCHAR(500),
	@ThemeID INT,
	@CreatorID INT,
	@ExpireDate DATETIME
AS
	INSERT INTO TaskTask(Name, Description, ThemeID, CreatorID, ExpireDate) 
			VALUES(@Name, @Description, @ThemeID, @CreatorID, @ExpireDate)
	 EXEC TaskProcedureAddTaskVersion 0, 1, @@IDENTITY, NULL
GO
