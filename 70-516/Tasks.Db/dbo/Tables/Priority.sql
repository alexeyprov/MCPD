CREATE TABLE [dbo].[Priority]
(
    [PriorityId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    [Name] VARCHAR(30) NOT NULL,
    [Ordinal] TINYINT NOT NULL,
    [ts] ROWVERSION
)
