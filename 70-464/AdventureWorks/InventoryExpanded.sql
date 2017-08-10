WITH NumCTE(N) AS
(
    SELECT 1
    
     UNION ALL
     
    SELECT N+1
      FROM NumCTE
     WHERE N < 1000
)
SELECT C.N,
       ROW_NUMBER() OVER (PARTITION BY ProductID ORDER BY ProductID, LocationID, Shelf, Bin) RNUM,
       ProductID,
       LocationID, 
       Shelf,
       Bin,
       Quantity
  FROM Production.ProductInventory I
 INNER JOIN NumCTE C ON C.N BETWEEN 1 AND I.Quantity
OPTION (MAXRECURSION 0)