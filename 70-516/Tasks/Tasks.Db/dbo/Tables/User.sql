CREATE TABLE [dbo].[User]
(
    [UserId] UNIQUEIDENTIFIER PRIMARY KEY,
    [FirstName] VARCHAR(30) NOT NULL,
    [LastName] VARCHAR(30) NOT NULL,
    [ts] ROWVERSION,
    --FOREIGN KEY ([UserId]) REFERENCES [$(AspNetDb)].[dbo].[aspnet_Users].[UserId]
)
