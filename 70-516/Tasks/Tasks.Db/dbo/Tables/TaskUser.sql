CREATE TABLE [dbo].[TaskUser]
(
    TaskId INT NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY NONCLUSTERED ([TaskId], [UserId]),
    FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Task] ([TaskId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
)
