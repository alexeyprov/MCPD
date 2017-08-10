USE [AdventureWorks]
GO

/****** Object:  Table [Production].[HierBillOfMaterials]    Script Date: 08/25/2013 23:09:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Production].[HierBillOfMaterials]') AND type in (N'U'))
DROP TABLE [Production].[HierBillOfMaterials]
GO

USE [AdventureWorks]
GO

/****** Object:  Table [Production].[HierBillOfMaterials]    Script Date: 08/25/2013 23:09:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [Production].[HierBillOfMaterials](
	[BomNode] [hierarchyid] NOT NULL,
	[ProductAssemblyID] [int] NULL,
	[ComponentID] [int] NULL,
	[UnitMeasureCode] [nchar](3) NULL,
	[PerAssemblyQty] [decimal](8, 2) NULL,
	[BomLevel]  AS ([BomNode].[GetLevel]()),
PRIMARY KEY NONCLUSTERED 
(
	[BomNode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

