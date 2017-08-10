CREATE VIEW [HumanResources].[vEmployeeDepartmentHistory] 
AS 
SELECT 
e.[EmployeeID] 
,c.[Title] 
,c.[FirstName] 
,c.[MiddleName] 
,c.[LastName] 
,c.[Suffix] 
,s.[Name] AS [Shift]
,d.[Name] AS [Department] 
,d.[GroupName] 
,edh.[StartDate] 
,edh.[EndDate]
FROM [HumanResources].[Employee] e
INNER JOIN [Person].[Contact] c 
ON c.[ContactID] = e.[ContactID]
INNER JOIN [HumanResources].[EmployeeDepartmentHistory] edh 
ON e.[EmployeeID] = edh.[EmployeeID] 
INNER JOIN [HumanResources].[Department] d 
ON edh.[DepartmentID] = d.[DepartmentID] 
INNER JOIN [HumanResources].[Shift] s
ON s.[ShiftID] = edh.[ShiftID];
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Returns employee name and current and previous departments.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'VIEW', @level1name = N'vEmployeeDepartmentHistory';

