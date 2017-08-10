WITH BomChildren(ProductAssemblyID, ComponentID)
AS
(
    SELECT ProductAssemblyID,
           ComponentID
      FROM Production.BillOfMaterials
     GROUP BY ProductAssemblyID,
              ComponentID
),
BomPaths(Path, ProductAssemblyID, ComponentID)
AS
(
    SELECT hierarchyid::GetRoot(),
           NULL,
           NULL
           
     UNION ALL
     
    SELECT CAST(
                '/' +
                CAST(ComponentID AS VARCHAR) +
                '/'
                AS hierarchyid) Path,
           NULL,
           ComponentID
      FROM BomChildren
     WHERE ProductAssemblyID IS NULL
     
     UNION ALL
     
     SELECT CAST(
                bp.Path.ToString() +
                CAST(bc.ComponentID AS VARCHAR) +
                '/'
                AS hierarchyid) Path,
            bc.ProductAssemblyID,
            bc.ComponentID
       FROM BomChildren bc
      INNER JOIN BomPaths bp ON bp.ComponentID = bc.ProductAssemblyID
 )
 INSERT INTO Production.HierBillOfMaterials
 SELECT bp.Path,
        bp.ProductAssemblyID,
        bp.ComponentID,
        bom.UnitMeasureCode,
        bom.PerAssemblyQty
   FROM BomPaths bp
   LEFT JOIN Production.BillOfMaterials bom 
     ON COALESCE(bom.ProductAssemblyID, -1) = COALESCE(bp.ProductAssemblyID, -1)
    AND bom.ComponentID = bp.ComponentID
  WHERE bom.EndDate IS NULL
  GROUP BY bp.Path,
        bp.ProductAssemblyID,
        bp.ComponentID,
        bom.UnitMeasureCode,
        bom.PerAssemblyQty
 GO