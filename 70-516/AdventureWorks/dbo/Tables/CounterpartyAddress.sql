CREATE TABLE [dbo].[CounterpartyAddress] (
    [CounterpartyId] INT              NOT NULL,
    [AddressID]      INT              NOT NULL,
    [AddressTypeID]  INT              NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_CustomerAddress_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_CustomerAddress_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_CounterpartyAddress_CounterpartyID_AddressID] PRIMARY KEY CLUSTERED ([CounterpartyId] ASC, [AddressID] ASC),
    CONSTRAINT [FK_CounterpartyAddress_Counterparty_CounterpartyId] FOREIGN KEY ([CounterpartyId]) REFERENCES [dbo].[Counterparty] ([CounterpartyId]),
    CONSTRAINT [FK_CustomerAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Person].[Address] ([AddressID]),
    CONSTRAINT [FK_CustomerAddress_AddressType_AddressTypeID] FOREIGN KEY ([AddressTypeID]) REFERENCES [Person].[AddressType] ([AddressTypeID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_CustomerAddress_rowguid]
    ON [dbo].[CounterpartyAddress]([rowguid] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index. Used to support replication samples.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'INDEX', @level2name = N'AK_CustomerAddress_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of NEWID()', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'CONSTRAINT', @level2name = N'DF_CustomerAddress_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'CONSTRAINT', @level2name = N'DF_CustomerAddress_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing AddressType.AddressTypeID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'CONSTRAINT', @level2name = N'FK_CustomerAddress_AddressType_AddressTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Address.AddressID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'CONSTRAINT', @level2name = N'FK_CustomerAddress_Address_AddressID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Counterparty.CounterpartyId.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'CONSTRAINT', @level2name = N'FK_CounterpartyAddress_Counterparty_CounterpartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'CONSTRAINT', @level2name = N'PK_CounterpartyAddress_CounterpartyID_AddressID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Address type. Foreign key to AddressType.AddressTypeID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'COLUMN', @level2name = N'AddressTypeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key. Foreign key to Address.AddressID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'COLUMN', @level2name = N'AddressID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key. Foreign key to Customer.CustomerID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress', @level2type = N'COLUMN', @level2name = N'CounterpartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cross-reference table mapping customers to their address(es).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CounterpartyAddress';

