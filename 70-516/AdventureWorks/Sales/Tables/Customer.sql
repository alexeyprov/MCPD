CREATE TABLE [Sales].[Customer] (
    [CustomerID]   INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TerritoryID]  INT              NULL,
    [CustomerType] NCHAR (1)        NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_Customer_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_Customer_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Customer_CustomerID] PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    CONSTRAINT [CK_Customer_CustomerType] CHECK (upper([CustomerType])='I' OR upper([CustomerType])='S'),
    CONSTRAINT [FK_Customer_Counterparty_CustomerId] FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Counterparty] ([CounterpartyId]),
    CONSTRAINT [FK_Customer_SalesTerritory_TerritoryID] FOREIGN KEY ([TerritoryID]) REFERENCES [Sales].[SalesTerritory] ([TerritoryID])
);


GO
CREATE NONCLUSTERED INDEX [IX_Customer_TerritoryID]
    ON [Sales].[Customer]([TerritoryID] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_Customer_rowguid]
    ON [Sales].[Customer]([rowguid] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nonclustered index.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'INDEX', @level2name = N'IX_Customer_TerritoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index. Used to support replication samples.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'INDEX', @level2name = N'AK_Customer_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Check constraint [CustomerType]=''I'' OR [CustomerType]=''i'' OR [CustomerType]=''S'' OR [CustomerType]=''s''', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'CONSTRAINT', @level2name = N'CK_Customer_CustomerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'CONSTRAINT', @level2name = N'DF_Customer_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of NEWID()', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'CONSTRAINT', @level2name = N'DF_Customer_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing SalesTerritory.TerritoryID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'CONSTRAINT', @level2name = N'FK_Customer_SalesTerritory_TerritoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'CONSTRAINT', @level2name = N'PK_Customer_CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Customer type: I = Individual, S = Store', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CustomerType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID of the territory in which the customer is located. Foreign key to SalesTerritory.SalesTerritoryID.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'TerritoryID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key for Customer records.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer', @level2type = N'COLUMN', @level2name = N'CustomerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Current customer information. Also see the Individual and Store tables.', @level0type = N'SCHEMA', @level0name = N'Sales', @level1type = N'TABLE', @level1name = N'Customer';

