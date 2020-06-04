USE intership

CREATE TABLE [TaskTheme] 
(
    [ID]   INT        IDENTITY NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
	CONSTRAINT [PK_Theme] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TaskStatus] 
(
    [ID]   tinyint        IDENTITY NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
	CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TaskEmployee] 
(
    [ID]   INT        IDENTITY NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
	CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [TaskTask]
(
 [ID]          INT IDENTITY  NOT NULL ,
 [Name]        NVARCHAR(100) NOT NULL ,
 [Description] NVARCHAR(500) NOT NULL ,
 [ThemeID]    int NOT NULL ,
 [CreatorID]  int NOT NULL ,
 [CreateDate]  date NOT NULL ,
 [ExpireDate]  date NOT NULL ,


 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_TaskToTheme] FOREIGN KEY ([ThemeID])  REFERENCES [TaskTheme]([ID]),
 CONSTRAINT [FK_TaskToEmployee] FOREIGN KEY ([CreatorID])  REFERENCES [TaskEmployee]([ID])
);
GO

CREATE TABLE [TaskTaskVersion]
(
 [ID]           INT NOT NULL ,
 [MoneyAward]   money NULL ,
 [Version]      tinyint NOT NULL ,
 [StatusID]    tinyint NOT NULL ,
 [TaskID]      int NOT NULL ,
 [PerformerID] int NULL ,


 CONSTRAINT [PK_TaskVersion] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_TaskVersionToStatus] FOREIGN KEY ([StatusID])  REFERENCES [TaskStatus]([ID]),
 CONSTRAINT [FK_TaskVersionToTask] FOREIGN KEY ([TaskID])  REFERENCES [TaskTask]([ID]),
 CONSTRAINT [FK_TaskVersionToEmployee] FOREIGN KEY ([PerformerID])  REFERENCES [TaskEmployee]([ID])
);
GO
CREATE TABLE [TaskComment]
(
 [Message]     nvarchar(300) NOT NULL ,
 [TaskID]     int NOT NULL ,
 [EmployeeID] int NOT NULL ,
 
 CONSTRAINT [FK_TaskID] FOREIGN KEY ([TaskID])  REFERENCES [TaskTask]([ID]),
 CONSTRAINT [FK_EmployeeID] FOREIGN KEY ([EmployeeID])  REFERENCES [TaskEmployee]([ID])
);
GO








