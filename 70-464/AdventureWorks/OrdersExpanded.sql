WITH NumCTE(N)
AS
(
    SELECT 1
    
     UNION ALL
     
    SELECT N + 1
      FROM NumCTE
     WHERE N < 1000
)
SELECT C.N,
       ROW_NUMBER() OVER (PARTITION BY ProductID ORDER BY ProductID, SalesOrderID) AS RNUM,
       SalesOrderDetailID,
       ProductID,
       SalesOrderID,
       OrderQty
  FROM SalesLT.SalesOrderDetail D
 INNER JOIN NumCTE C ON C.N BETWEEN 1 AND D.OrderQty
OPTION (MAXRECURSION 0)