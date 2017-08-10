CREATE PROCEDURE [dbo].[uspGetManagerEmployees]
@ManagerID [int]
AS
BEGIN
SET NOCOUNT ON;
WITH [EMP_cte]([EmployeeID], [ManagerID], [FirstName], [LastName], [RecursionLevel]) -- CTE name and columns
AS (
SELECT e.[EmployeeID], e.[ManagerID], c.[FirstName], c.[LastName], 0 -- Get the initial list of Employees for Manager n
FROM [HumanResources].[Employee] e 
INNER JOIN [Person].[Contact] c 
ON e.[ContactID] = c.[ContactID]
WHERE [ManagerID] = @ManagerID
UNION ALL
SELECT e.[EmployeeID], e.[ManagerID], c.[FirstName], c.[LastName], [RecursionLevel] + 1 -- Join recursive member to anchor
FROM [HumanResources].[Employee] e 
INNER JOIN [EMP_cte]
ON e.[ManagerID] = [EMP_cte].[EmployeeID]
INNER JOIN [Person].[Contact] c 
ON e.[ContactID] = c.[ContactID]
)
SELECT [EMP_cte].[RecursionLevel], [EMP_cte].[ManagerID], c.[FirstName] AS 'ManagerFirstName', c.[LastName] AS 'ManagerLastName',
[EMP_cte].[EmployeeID], [EMP_cte].[FirstName], [EMP_cte].[LastName] -- Outer select from the CTE
FROM [EMP_cte] 
INNER JOIN [HumanResources].[Employee] e 
ON [EMP_cte].[ManagerID] = e.[EmployeeID]
INNER JOIN [Person].[Contact] c 
ON e.[ContactID] = c.[ContactID]
ORDER BY [RecursionLevel], [ManagerID], [EmployeeID]
OPTION (MAXRECURSION 25) 
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspGetManagerEmployees. Enter a valid ManagerID from the HumanResources.Employee table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'uspGetManagerEmployees', @level2type = N'PARAMETER', @level2name = N'@ManagerID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Stored procedure using a recursive query to return the direct and indirect employees of the specified manager.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'uspGetManagerEmployees';

