CREATE TABLE [dbo].[Category]
(
    [CategoryId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(30) NOT NULL,
    [Description] VARCHAR(100),
    [ts] ROWVERSION
)
