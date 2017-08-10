CREATE PROCEDURE [dbo].[uspGetEmployeeManagers]
@EmployeeID [int]
AS
BEGIN
SET NOCOUNT ON;
WITH [EMP_cte]([EmployeeID], [ManagerID], [FirstName], [LastName], [Title], [RecursionLevel]) -- CTE name and columns
AS (
SELECT e.[EmployeeID], e.[ManagerID], c.[FirstName], c.[LastName], e.[Title], 0 -- Get the initial Employee
FROM [HumanResources].[Employee] e 
INNER JOIN [Person].[Contact] c 
ON e.[ContactID] = c.[ContactID]
WHERE e.[EmployeeID] = @EmployeeID
UNION ALL
SELECT e.[EmployeeID], e.[ManagerID], c.[FirstName], c.[LastName], e.[Title], [RecursionLevel] + 1 -- Join recursive member to anchor
FROM [HumanResources].[Employee] e 
INNER JOIN [EMP_cte]
ON e.[EmployeeID] = [EMP_cte].[ManagerID]
INNER JOIN [Person].[Contact] c 
ON e.[ContactID] = c.[ContactID]
)
SELECT [EMP_cte].[RecursionLevel], [EMP_cte].[EmployeeID], [EMP_cte].[FirstName], [EMP_cte].[LastName], 
[EMP_cte].[ManagerID], c.[FirstName] AS 'ManagerFirstName', c.[LastName] AS 'ManagerLastName'  -- Outer select from the CTE
FROM [EMP_cte] 
INNER JOIN [HumanResources].[Employee] e 
ON [EMP_cte].[ManagerID] = e.[EmployeeID]
INNER JOIN [Person].[Contact] c 
ON e.[ContactID] = c.[ContactID]
ORDER BY [RecursionLevel], [ManagerID], [EmployeeID]
OPTION (MAXRECURSION 25) 
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the stored procedure uspGetEmployeeManagers. Enter a valid EmployeeID from the HumanResources.Employee table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'uspGetEmployeeManagers', @level2type = N'PARAMETER', @level2name = N'@EmployeeID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Stored procedure using a recursive query to return the direct and indirect managers of the specified employee.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'uspGetEmployeeManagers';

