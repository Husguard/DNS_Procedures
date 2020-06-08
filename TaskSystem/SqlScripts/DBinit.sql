USE Shevelev
ALTER DATABASE Shevelev
COLLATE Cyrillic_General_CI_AS
GO
CREATE TABLE [TaskTheme] 
(
    [ID]   INT        IDENTITY NOT NULL,
    [Name] NVARCHAR (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
	CONSTRAINT [PK_Theme] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TaskStatus] 
(
    [ID]   tinyint        IDENTITY NOT NULL,
    [Name] NVARCHAR (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
	CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TaskEmployee] 
(
    [ID]   INT        IDENTITY NOT NULL,
	[Login] NVARCHAR (100) UNIQUE NOT NULL,
    [Name] NVARCHAR (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
	CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TaskTask]
(
 [ID]          INT IDENTITY  NOT NULL ,
 [Name]        NVARCHAR(100) COLLATE Cyrillic_General_CI_AS NOT NULL ,
 [Description] NVARCHAR(500) COLLATE Cyrillic_General_CI_AS NOT NULL ,
 [ThemeID]    int NOT NULL ,
 [CreatorID]  int NOT NULL ,
 [CreateDate]  date NOT NULL ,
 [ExpireDate]  date NOT NULL ,


 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_TaskToTheme] FOREIGN KEY ([ThemeID])  REFERENCES [TaskTheme]([ID]),
 CONSTRAINT [FK_TaskToEmployee] FOREIGN KEY ([CreatorID])  REFERENCES [TaskEmployee]([ID]),

 INDEX [IX_TaskTaskThemeID] NONCLUSTERED ([ThemeID]),
 INDEX [IX_TaskTaskCreatorID] NONCLUSTERED ([CreatorID])
);
GO

CREATE TABLE [TaskTaskVersion]
(
 [ID]           INT IDENTITY NOT NULL ,
 [MoneyAward]   money NULL ,
 [Version]      tinyint NOT NULL ,
 [StatusID]    tinyint NOT NULL ,
 [TaskID]      int NOT NULL ,
 [PerformerID] int NULL ,

 UNIQUE([TaskID],[Version]),
 CONSTRAINT [PK_TaskVersion] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_TaskVersionToStatus] FOREIGN KEY ([StatusID]) REFERENCES  [TaskStatus]([ID]),
 CONSTRAINT [FK_TaskVersionToTask] FOREIGN KEY ([TaskID])  REFERENCES [TaskTask]([ID]),
 CONSTRAINT [FK_TaskVersionToEmployee] FOREIGN KEY ([PerformerID])  REFERENCES [TaskEmployee]([ID]),

 INDEX [IX_TaskTaskVersionStatusID] NONCLUSTERED ([StatusID]),
 INDEX [IX_TaskTaskVersionPerformerID] NONCLUSTERED ([PerformerID])
);
GO
CREATE TABLE [TaskComment]
(
 [Message]     nvarchar(300) COLLATE Cyrillic_General_CI_AS NOT NULL ,
 [TaskID]     int NOT NULL ,
 [EmployeeID] int NOT NULL ,
 [CreateDate] Datetime NOT NULL
 
 CONSTRAINT [FK_TaskID] FOREIGN KEY ([TaskID])  REFERENCES [TaskTask]([ID]),
 CONSTRAINT [FK_EmployeeID] FOREIGN KEY ([EmployeeID])  REFERENCES [TaskEmployee]([ID]),
 INDEX [IX_TaskCommentTaskID] NONCLUSTERED ([TaskID])
);
GO