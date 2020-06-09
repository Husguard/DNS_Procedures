-- Шевелев Максим
-- 09.06.2020
-- Изменить выполняющего у версии задания
CREATE PROCEDURE [dbo].[TaskProcedureUpdatePerformerOfTask] 
	@TaskID INT,
	@Version TINYINT,
	@EmployeeID INT
AS
	 UPDATE TaskTaskVersion SET PerformerID = @EmployeeID
		WHERE TaskTaskVersion.TaskID = @TaskID AND TaskTaskVersion.Version = @Version
GO

