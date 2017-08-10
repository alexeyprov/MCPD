CREATE TABLE [Sales].[StoreContact] (
    [CustomerID]    INT              NOT NULL,
    [ContactID]     INT              NOT NULL,
    [ContactTypeID] INT              NOT NULL,
    [rowguid]       UNIQUEIDENTIFIER CONSTRAINT [DF_StoreContact_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME         CONSTRAINT [DF_StoreContact_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_StoreContact_CustomerID_ContactID] PRIMARY KEY CLUSTERED ([CustomerID] ASC, [ContactID] ASC),
    CONSTRAINT [FK_StoreContact_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [Person].[Contact] ([ContactID]),
    CONSTRAINT [FK_StoreContact_ContactType_ContactTypeID] FOREIGN KEY ([ContactTypeID]) REFERENCES [Person].[ContactType] ([ContactTypeID]),
    CONSTRAINT [FK_StoreContact_Store_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Sales].[Store] ([CustomerID])
);


GO
CREATE NONCLUSTERED INDEX [IX_StoreContact_ContactTypeID]
    ON [Sales].[StoreContact]([ContactTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StoreContact_ContactID]
    ON [Sales].[StoreContact]([ContactID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_StoreContact_rowguid]
    ON [Sales].[StoreContact]([rowguid] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'INDEX', @level2name = N'IX_StoreContact_ContactTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'INDEX', @level2name = N'IX_StoreContact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index. Used to support replication samples.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'INDEX', @level2name = N'AK_StoreContact_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'CONSTRAINT', @level2name = N'DF_StoreContact_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of NEWID()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'CONSTRAINT', @level2name = N'DF_StoreContact_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Store.CustomerID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'CONSTRAINT', @level2name = N'FK_StoreContact_Store_CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing ContactType.ContactTypeID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'CONSTRAINT', @level2name = N'FK_StoreContact_ContactType_ContactTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'CONSTRAINT', @level2name = N'FK_StoreContact_Contact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'CONSTRAINT', @level2name = N'PK_StoreContact_CustomerID_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contact type such as owner or purchasing agent. Foreign key to ContactType.ContactTypeID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'COLUMN', @level2name = N'ContactTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contact (store employee) identification number. Foreign key to Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'COLUMN', @level2name = N'ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Store identification number. Foreign key to Customer.CustomerID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact', @level2type = N'COLUMN', @level2name = N'CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cross-reference table mapping stores and their employees.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'StoreContact';

