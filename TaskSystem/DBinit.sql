USE intership
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [TASK_Theme] 
(
    [ID]   INT        IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
	CONSTRAINT [PK_Theme] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TASK_Status] 
(
    [ID]   tinyint        IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
	CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TASK_Employee] 
(
    [ID]   INT        IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
	CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TASK_Task]
(
 [ID]          INT IDENTITY (1, 1) NOT NULL ,
 [Name]        NVARCHAR(50) NOT NULL ,
 [Description] NVARCHAR(50) NOT NULL ,
 [ThemeID]    int NOT NULL ,
 [CreatorID]  int NOT NULL ,
 [CreateDate]  date NOT NULL ,
 [ExpireDate]  date NOT NULL ,


 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_ThemeID] FOREIGN KEY ([ThemeID])  REFERENCES [TASK_Theme]([ID]),
 CONSTRAINT [FK_CreatorID] FOREIGN KEY ([CreatorID])  REFERENCES [TASK_Employee]([ID])
);
GO

CREATE TABLE [TASK_TaskVersion]
(
 [ID]           INT NOT NULL ,
 [MoneyAward]   money NULL ,
 [Version]      tinyint NOT NULL ,
 [StatusID]    tinyint NOT NULL ,
 [TaskID]      int NOT NULL ,
 [PerformerID] int NULL ,


 CONSTRAINT [PK_TaskVersion] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_StatusID] FOREIGN KEY ([StatusID])  REFERENCES [TASK_Status]([ID]),
 CONSTRAINT [FK_TaskID] FOREIGN KEY ([TaskID])  REFERENCES [TASK_Task]([ID]),
 CONSTRAINT [FK_PerformerID] FOREIGN KEY ([PerformerID])  REFERENCES [TASK_Employee]([ID])
);
GO
CREATE TABLE [TASK_Comment]
(
 [Message]     nvarchar(50) NOT NULL ,
 [TaskID]     int NOT NULL ,
 [EmployeeID] int NOT NULL ,


 CONSTRAINT [FK_TaskID] FOREIGN KEY ([TaskID])  REFERENCES [TASK_Task]([ID]),
 CONSTRAINT [FK_EmployeeID] FOREIGN KEY ([EmployeeID])  REFERENCES [TASK_Employee]([ID])
);
GO








