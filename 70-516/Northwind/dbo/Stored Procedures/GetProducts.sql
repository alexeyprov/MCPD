SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE [dbo].[GetProducts]
AS
BEGIN
    SET ANSI_NULLS ON;
    SET ANSI_PADDING ON;
    SET ANSI_WARNINGS ON;
    SET CONCAT_NULL_YIELDS_NULL ON;
    SET QUOTED_IDENTIFIER ON;
    SET NUMERIC_ROUNDABORT OFF;
    SET ARITHABORT ON;

    SELECT [ProductID],
           [ProductName],
           [SupplierID],
           [CategoryID],
           [QuantityPerUnit],
           [UnitPrice],
           [UnitsInStock],
           [UnitsOnOrder],
           [ReorderLevel],
           [Discontinued]
      FROM [dbo].[Products];

END