CREATE TABLE [Production].[HierBillOfMaterials] (
    [BomNode]           [sys].[hierarchyid] NOT NULL,
    [ProductAssemblyID] INT                 NULL,
    [ComponentID]       INT                 NULL,
    [UnitMeasureCode]   NCHAR (3)           NULL,
    [PerAssemblyQty]    DECIMAL (8, 2)      NULL,
    [BomLevel]          AS                  ([BomNode].[GetLevel]()),
    PRIMARY KEY NONCLUSTERED ([BomNode] ASC)
);

