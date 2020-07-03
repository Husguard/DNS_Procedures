-- Шевелев Максим
-- 03.07.2020
-- Создание расписания, в котором запускается выполнение задания, добавляющее версии для устаревших заданий
USE [TaskManage]
GO

/****** Object:  Job [TaskJobAddOutdatedVersion]    Script Date: 03.07.2020 15:02:52 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [Database Maintenance]    Script Date: 03.07.2020 15:02:52 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'TaskJobAddOutdatedVersion', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Описание недоступно.', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'PARTNER\Shevelev.MS', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Test]    Script Date: 03.07.2020 15:02:52 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Test', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'DECLARE  @TaskId    INT
DECLARE TaskCursorOutdatedTasks CURSOR LOCAL FOR
	SELECT 
		ttt.TaskID
		FROM 
		(
			SELECT 
				ttv.TaskID, 
				MAX(ttv.Version) AS LastVersion 
			FROM TaskTaskVersion AS ttv 
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
			WHERE ExpireDate < GETDATE()
			GROUP BY TaskID
		) AS LastResult
		INNER JOIN 
			(
			SELECT 
				ttv.Version,
				ttv.TaskID,
				tta.ExpireDate
			FROM TaskTaskVersion AS ttv
			INNER JOIN TaskTask AS tta ON tta.ID = ttv.TaskID
			WHERE ttv.StatusID != 5
			) AS ttt ON LastResult.TaskID = ttt.TaskID AND LastResult.LastVersion = ttt.Version
OPEN TaskCursorOutdatedTasks
FETCH NEXT FROM TaskCursorOutdatedTasks INTO @TaskId
WHILE @@FETCH_STATUS=0
BEGIN
	EXEC TaskProcedureAddTaskVersion 0,5,@TaskId,NULL
    FETCH NEXT FROM TaskCursorOutdatedTasks 
      INTO  @TaskId
END
CLOSE TaskCursorOutdatedTasks
DEALLOCATE TaskCursorOutdatedTasks', 
		@database_name=N'TaskManage', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'TaskScheduleAddOutdatedVersion', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval=30, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20200703, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, 
		@schedule_uid=N'e467f4a0-c29b-4e4f-8ceb-395e3a72ac31'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
GO


