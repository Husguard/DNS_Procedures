-- Шевелев Максим
-- 09.06.2020
-- Изменить статус у определенной версии определенного задания
CREATE PROCEDURE [dbo].[TaskProcedureUpdateStatusOfTask]
	@TaskID INT,
	@Version TINYINT,
	@StatusID TINYINT
AS
	 UPDATE TaskTaskVersion SET StatusID = @StatusID 
		WHERE TaskTaskVersion.TaskID = @TaskID AND TaskTaskVersion.Version = @Version
GO

