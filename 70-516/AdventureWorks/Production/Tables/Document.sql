CREATE TABLE [Production].[Document] (
    [DocumentID]      INT             IDENTITY (1, 1) NOT NULL,
    [Title]           NVARCHAR (50)   NOT NULL,
    [FileName]        NVARCHAR (400)  NOT NULL,
    [FileExtension]   NVARCHAR (8)    NOT NULL,
    [Revision]        NCHAR (5)       NOT NULL,
    [ChangeNumber]    INT             CONSTRAINT [DF_Document_ChangeNumber] DEFAULT ((0)) NOT NULL,
    [Status]          TINYINT         NOT NULL,
    [DocumentSummary] NVARCHAR (MAX)  NULL,
    [Document]        VARBINARY (MAX) NULL,
    [ModifiedDate]    DATETIME        CONSTRAINT [DF_Document_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Document_DocumentID] PRIMARY KEY CLUSTERED ([DocumentID] ASC),
    CONSTRAINT [CK_Document_Status] CHECK ([Status]>=(1) AND [Status]<=(3))
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_Document_FileName_Revision]
    ON [Production].[Document]([FileName] ASC, [Revision] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'INDEX', @level2name = N'AK_Document_FileName_Revision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Check constraint [Status] BETWEEN (1) AND (3)', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'CONSTRAINT', @level2name = N'CK_Document_Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of 0', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'CONSTRAINT', @level2name = N'DF_Document_ChangeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'CONSTRAINT', @level2name = N'DF_Document_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'CONSTRAINT', @level2name = N'PK_Document_DocumentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complete document.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'Document';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Document abstract.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'DocumentSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 = Pending approval, 2 = Approved, 3 = Obsolete', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Engineering change approval number.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'ChangeNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Revision number of the document. ', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'Revision';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'File extension indicating the document type. For example, .doc or .txt.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'FileExtension';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Directory path and file name of the document', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'FileName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Title of the document.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key for Document records.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document', @level2type = N'COLUMN', @level2name = N'DocumentID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Product maintenance documents.', @level0type = N'SCHEMA', @level0name = N'Production', @level1type = N'TABLE', @level1name = N'Document';

