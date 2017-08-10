CREATE TABLE [HumanResources].[EmployeeAddress] (
    [EmployeeID]   INT              NOT NULL,
    [AddressID]    INT              NOT NULL,
    [rowguid]      UNIQUEIDENTIFIER CONSTRAINT [DF_EmployeeAddress_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate] DATETIME         CONSTRAINT [DF_EmployeeAddress_ModifiedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_EmployeeAddress_EmployeeID_AddressID] PRIMARY KEY CLUSTERED ([EmployeeID] ASC, [AddressID] ASC),
    CONSTRAINT [FK_EmployeeAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Person].[Address] ([AddressID]),
    CONSTRAINT [FK_EmployeeAddress_Employee_EmployeeID] FOREIGN KEY ([EmployeeID]) REFERENCES [HumanResources].[Employee] ([EmployeeID])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [AK_EmployeeAddress_rowguid]
    ON [HumanResources].[EmployeeAddress]([rowguid] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unique nonclustered index. Used to support replication samples.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'INDEX', @level2name = N'AK_EmployeeAddress_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of NEWID()', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'CONSTRAINT', @level2name = N'DF_EmployeeAddress_rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default constraint value of GETDATE()', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'CONSTRAINT', @level2name = N'DF_EmployeeAddress_ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Employee.EmployeeID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'CONSTRAINT', @level2name = N'FK_EmployeeAddress_Employee_EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foreign key constraint referencing Address.AddressID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'CONSTRAINT', @level2name = N'FK_EmployeeAddress_Address_AddressID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key (clustered) constraint', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'CONSTRAINT', @level2name = N'PK_EmployeeAddress_EmployeeID_AddressID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Date and time the record was last updated.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'COLUMN', @level2name = N'rowguid';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key. Foreign key to Address.AddressID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'COLUMN', @level2name = N'AddressID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primary key. Foreign key to Employee.EmployeeID.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress', @level2type = N'COLUMN', @level2name = N'EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cross-reference table mapping employees to their address(es).', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'TABLE', @level1name = N'EmployeeAddress';

