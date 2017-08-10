CREATE TABLE [dbo].[TaskCategory]
(
    [TaskId] INT NOT NULL,
    [CategoryId] INT NOT NULL,
    PRIMARY KEY NONCLUSTERED ([TaskId], [CategoryId]),
    FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([TaskId]),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId])
)
