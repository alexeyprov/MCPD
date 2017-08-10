CREATE TABLE [Person].[Contact] (
    [ContactID]             INT                                                           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NameStyle]             [dbo].[NameStyle]                                             CONSTRAINT [DF_Contact_NameStyle] DEFAULT ((0)) NOT NULL,
    [Title]                 NVARCHAR (8)                                                  NULL,
    [FirstName]             [dbo].[Name]                                                  NOT NULL,
    [MiddleName]            [dbo].[Name]                                                  NULL,
    [LastName]              [dbo].[Name]                                                  NOT NULL,
    [Suffix]                NVARCHAR (10)                                                 NULL,
    [EmailAddress]          NVARCHAR (50)                                                 NULL,
    [EmailPromotion]        INT                                                           CONSTRAINT [DF_Contact_EmailPromotion] DEFAULT ((0)) NOT NULL,
    [Phone]                 [dbo].[Phone]                                                 NULL,
    [PasswordHash]          VARCHAR (128)                                                 NOT NULL,
    [PasswordSalt]          VARCHAR (10)                                                  NOT NULL,
    [AdditionalContactInfo] XML(CONTENT [Person].[AdditionalContactInfoSchemaCollection]) NULL,
    [rowguid]               UNIQUEIDENTIFIER                                              CONSTRAINT [DF_Contact_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]          DATETIME                                                      CONSTRAINT [DF_Contact_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Contact_ContactID] PRIMARY KEY CLUSTERED ([ContactID] ASC),
    CONSTRAINT [CK_Contact_EmailPromotion] CHECK ([EmailPromotion]>=(0) AND [EmailPromotion]<=(2))
);


GO
CREATE NONCLUSTERED INDEX [IX_Contact_EmailAddress]
    ON [Person].[Contact]([EmailAddress] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_Contact_rowguid]
    ON [Person].[Contact]([rowguid] ASC);


GO
CREATE PRIMARY XML INDEX [PXML_Contact_AddContact]
    ON [Person].[Contact]([AdditionalContactInfo])
    WITH (PAD_INDEX = OFF);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary XML index.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'INDEX', @level2name = N'PXML_Contact_AddContact';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'INDEX', @level2name = N'IX_Contact_EmailAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index. Used to support replication samples.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'INDEX', @level2name = N'AK_Contact_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Check constraint [EmailPromotion] >= (0) AND [EmailPromotion] <= (2)', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'CONSTRAINT', @level2name = N'CK_Contact_EmailPromotion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'CONSTRAINT', @level2name = N'DF_Contact_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of NEWID()', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'CONSTRAINT', @level2name = N'DF_Contact_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of 0', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'CONSTRAINT', @level2name = N'DF_Contact_EmailPromotion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of 0', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'CONSTRAINT', @level2name = N'DF_Contact_NameStyle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'CONSTRAINT', @level2name = N'PK_Contact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Additional contact information about the person stored in xml format. ', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'AdditionalContactInfo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Random value concatenated with the password string before the password is hashed.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'PasswordSalt';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Password for the e-mail account.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'PasswordHash';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Phone number associated with the person.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners. ', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'EmailPromotion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'E-mail address for the person.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'EmailAddress';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Surname suffix. For example, Sr. or Jr.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'Suffix';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Last name of the person.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'LastName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Middle name or middle initial of the person.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'MiddleName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'First name of the person.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'FirstName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A courtesy title. For example, Mr. or Ms.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'Title';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'NameStyle';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key for Contact records.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact', @level2type = N'COLUMN', @level2name = N'ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Names of each employee, customer contact, and vendor contact.', @level0type = N'SCHEMA', @level0name = N'Person', @level1type = N'TABLE', @level1name = N'Contact';

