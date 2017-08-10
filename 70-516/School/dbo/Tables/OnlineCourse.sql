CREATE TABLE [dbo].[OnlineCourse] (
    [CourseID] INT            NOT NULL,
    [URL]      NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_OnlineCourse] PRIMARY KEY CLUSTERED ([CourseID] ASC),
    CONSTRAINT [FK_OnlineCourse_Course] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID])
);

