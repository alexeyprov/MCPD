CREATE TABLE [dbo].[Task]
(
    [TaskId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [Subject] VARCHAR(100) NOT NULL,
    [StartDate] DATETIME,
    [DueDate] DATETIME,
    [DateCompleted] DATETIME,
    [PriorityId] INT NOT NULL,
    [StatusId] INT NOT NULL,
    [CreationDate] DATETIME NOT NULL,
    [ts] ROWVERSION,
    FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status] ([StatusId]),
    FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priority] ([PriorityId])
)
