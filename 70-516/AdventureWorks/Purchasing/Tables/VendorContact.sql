CREATE TABLE [Purchasing].[VendorContact] (
    [VendorID]      INT      NOT NULL,
    [ContactID]     INT      NOT NULL,
    [ContactTypeID] INT      NOT NULL,
    [ModifiedDate]  DATETIME CONSTRAINT [DF_VendorContact_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_VendorContact_VendorID_ContactID] PRIMARY KEY CLUSTERED ([VendorID] ASC, [ContactID] ASC),
    CONSTRAINT [FK_VendorContact_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [Person].[Contact] ([ContactID]),
    CONSTRAINT [FK_VendorContact_ContactType_ContactTypeID] FOREIGN KEY ([ContactTypeID]) REFERENCES [Person].[ContactType] ([ContactTypeID]),
    CONSTRAINT [FK_VendorContact_Vendor_VendorID] FOREIGN KEY ([VendorID]) REFERENCES [Purchasing].[Vendor] ([VendorId])
);


GO
CREATE NONCLUSTERED INDEX [IX_VendorContact_ContactTypeID]
    ON [Purchasing].[VendorContact]([ContactTypeID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_VendorContact_ContactID]
    ON [Purchasing].[VendorContact]([ContactID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'INDEX', @level2name = N'IX_VendorContact_ContactTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'INDEX', @level2name = N'IX_VendorContact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'CONSTRAINT', @level2name = N'DF_VendorContact_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing ContactType.ContactTypeID.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'CONSTRAINT', @level2name = N'FK_VendorContact_ContactType_ContactTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'CONSTRAINT', @level2name = N'FK_VendorContact_Contact_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'CONSTRAINT', @level2name = N'PK_VendorContact_VendorID_ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contact type such as sales manager, or sales agent.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'COLUMN', @level2name = N'ContactTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contact (Vendor employee) identification number. Foreign key to Contact.ContactID.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'COLUMN', @level2name = N'ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact', @level2type = N'COLUMN', @level2name = N'VendorID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cross-reference table mapping vendors and their employees.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'TABLE', @level1name = N'VendorContact';

