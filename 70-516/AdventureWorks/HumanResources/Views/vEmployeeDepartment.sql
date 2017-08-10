CREATE VIEW [HumanResources].[vEmployeeDepartment] 
AS 
SELECT 
e.[EmployeeID] 
,c.[Title] 
,c.[FirstName] 
,c.[MiddleName] 
,c.[LastName] 
,c.[Suffix] 
,e.[Title] AS [JobTitle] 
,d.[Name] AS [Department] 
,d.[GroupName] 
,edh.[StartDate] 
FROM [HumanResources].[Employee] e
INNER JOIN [Person].[Contact] c 
ON c.[ContactID] = e.[ContactID]
INNER JOIN [HumanResources].[EmployeeDepartmentHistory] edh 
ON e.[EmployeeID] = edh.[EmployeeID] 
INNER JOIN [HumanResources].[Department] d 
ON edh.[DepartmentID] = d.[DepartmentID] 
WHERE GETDATE() BETWEEN edh.[StartDate] AND ISNULL(edh.[EndDate], GETDATE());
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Returns employee name, title, and current department.', @level0type = N'SCHEMA', @level0name = N'HumanResources', @level1type = N'VIEW', @level1name = N'vEmployeeDepartment';

