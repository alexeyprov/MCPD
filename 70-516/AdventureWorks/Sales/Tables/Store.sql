CREATE TABLE [Sales].[Store] (
    [CustomerID]    INT                                                NOT NULL,
    [Name]          [dbo].[Name]                                       NOT NULL,
    [SalesPersonID] INT                                                NULL,
    [Demographics]  XML(CONTENT [Sales].[StoreSurveySchemaCollection]) NULL,
    [rowguid]       UNIQUEIDENTIFIER                                   CONSTRAINT [DF_Store_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]  DATETIME                                           CONSTRAINT [DF_Store_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Store_CustomerID] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [FK_Store_Customer_CustomerID] FOREIGN KEY ([CustomerID]) REFERENCES [Sales].[Customer] ([CustomerID]),
    CONSTRAINT [FK_Store_SalesPerson_SalesPersonID] FOREIGN KEY ([SalesPersonID]) REFERENCES [Sales].[SalesPerson] ([SalesPersonID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Store_SalesPersonID]
    ON [Sales].[Store]([SalesPersonID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_Store_rowguid]
    ON [Sales].[Store]([rowguid] ASC);


GO
CREATE PRIMARY XML INDEX [PXML_Store_Demographics]
    ON [Sales].[Store]([Demographics])
    WITH (PAD_INDEX = OFF);


GO
CREATE TRIGGER [Sales].[iStore] ON [Sales].[Store] 
AFTER INSERT AS 
BEGIN
DECLARE @Count int;
SET @Count = @@ROWCOUNT;
IF @Count = 0 
RETURN;
SET NOCOUNT ON;
BEGIN TRY
IF EXISTS (SELECT * FROM inserted INNER JOIN [Sales].[Individual] 
ON inserted.[CustomerID] = [Sales].[Individual].[CustomerID]) 
BEGIN
IF @@TRANCOUNT > 0
BEGIN
ROLLBACK TRANSACTION;
END
END;
END TRY
BEGIN CATCH
EXECUTE [dbo].[uspPrintError];
IF @@TRANCOUNT > 0
BEGIN
ROLLBACK TRANSACTION;
END
EXECUTE [dbo].[uspLogError];
END CATCH;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'AFTER INSERT trigger inserting Store only if the Customer does not exist in the Individual table.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'TRIGGER', @level2name = N'iStore';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary XML index.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'INDEX', @level2name = N'PXML_Store_Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'INDEX', @level2name = N'IX_Store_SalesPersonID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index. Used to support replication samples.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'INDEX', @level2name = N'AK_Store_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'CONSTRAINT', @level2name = N'DF_Store_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of NEWID()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'CONSTRAINT', @level2name = N'DF_Store_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing SalesPerson.SalesPersonID', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'CONSTRAINT', @level2name = N'FK_Store_SalesPerson_SalesPersonID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Customer.CustomerID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'CONSTRAINT', @level2name = N'FK_Store_Customer_CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'CONSTRAINT', @level2name = N'PK_Store_CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Demographic informationg about the store such as the number of employees, annual sales and store type.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'COLUMN', @level2name = N'Demographics';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID of the sales person assigned to the customer. Foreign key to SalesPerson.SalesPersonID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'COLUMN', @level2name = N'SalesPersonID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Name of the store.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key. Foreign key to Customer.CustomerID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store', @level2type = N'COLUMN', @level2name = N'CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Customers (resellers) of Adventure Works products.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Store';

