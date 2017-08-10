CREATE TABLE [HumanResources].[JobCandidate] (
    [JobCandidateID] INT                                                      IDENTITY (1, 1) NOT NULL,
    [EmployeeID]     INT                                                      NULL,
    [Resume]         XML(CONTENT [HumanResources].[HRResumeSchemaCollection]) NULL,
    [ModifiedDate]   DATETIME                                                 CONSTRAINT [DF_JobCandidate_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_JobCandidate_JobCandidateID] PRIMARY KEY CLUSTERED ([JobCandidateID] ASC),
    CONSTRAINT [FK_JobCandidate_Employee_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [HumanResources].[Employee] ([EmployeeID])
);


GO
CREATE NONCLUSTERED INDEX [IX_JobCandidate_EmployeeID]
    ON [HumanResources].[JobCandidate]([EmployeeID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'INDEX', @level2name = N'IX_JobCandidate_EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'CONSTRAINT', @level2name = N'DF_JobCandidate_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Employee.EmployeeID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'CONSTRAINT', @level2name = N'FK_JobCandidate_Employee_EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'CONSTRAINT', @level2name = N'PK_JobCandidate_JobCandidateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Résumé in XML format.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'COLUMN', @level2name = N'Resume';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Employee identification number if applicant was hired. Foreign key to Employee.EmployeeID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'COLUMN', @level2name = N'EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key for JobCandidate records.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate', @level2type = N'COLUMN', @level2name = N'JobCandidateID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Résumés submitted to Human Resources by job applicants.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'JobCandidate';

