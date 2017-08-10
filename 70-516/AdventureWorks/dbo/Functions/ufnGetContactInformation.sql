﻿CREATE FUNCTION [dbo].[ufnGetContactInformation](@ContactID int)
RETURNS @retContactInformation TABLE 
(
[ContactID] int PRIMARY KEY NOT NULL, 
[FirstName] [nvarchar](50) NULL, 
[LastName] [nvarchar](50) NULL, 
[JobTitle] [nvarchar](50) NULL, 
[ContactType] [nvarchar](50) NULL
)
AS 
BEGIN
DECLARE 
@FirstName [nvarchar](50), 
@LastName [nvarchar](50), 
@JobTitle [nvarchar](50), 
@ContactType [nvarchar](50);
SELECT 
@ContactID = ContactID, 
@FirstName = FirstName, 
@LastName = LastName
FROM [Person].[Contact] 
WHERE [ContactID] = @ContactID;
SET @JobTitle = 
CASE 
WHEN EXISTS(SELECT * FROM [HumanResources].[Employee] e 
WHERE e.[ContactID] = @ContactID) 
THEN (SELECT [Title] 
FROM [HumanResources].[Employee] 
WHERE [ContactID] = @ContactID)
WHEN EXISTS(SELECT * FROM [Purchasing].[VendorContact] vc 
INNER JOIN [Person].[ContactType] ct 
ON vc.[ContactTypeID] = ct.[ContactTypeID] 
WHERE vc.[ContactID] = @ContactID) 
THEN (SELECT ct.[Name] 
FROM [Purchasing].[VendorContact] vc 
INNER JOIN [Person].[ContactType] ct 
ON vc.[ContactTypeID] = ct.[ContactTypeID] 
WHERE vc.[ContactID] = @ContactID)
WHEN EXISTS(SELECT * FROM [Sales].[StoreContact] sc 
INNER JOIN [Person].[ContactType] ct 
ON sc.[ContactTypeID] = ct.[ContactTypeID] 
WHERE sc.[ContactID] = @ContactID) 
THEN (SELECT ct.[Name] 
FROM [Sales].[StoreContact] sc 
INNER JOIN [Person].[ContactType] ct 
ON sc.[ContactTypeID] = ct.[ContactTypeID] 
WHERE [ContactID] = @ContactID)
ELSE NULL 
END;
SET @ContactType = 
CASE 
WHEN EXISTS(SELECT * FROM [HumanResources].[Employee] e 
WHERE e.[ContactID] = @ContactID) 
THEN 'Employee'
WHEN EXISTS(SELECT * FROM [Purchasing].[VendorContact] vc 
INNER JOIN [Person].[ContactType] ct 
ON vc.[ContactTypeID] = ct.[ContactTypeID] 
WHERE vc.[ContactID] = @ContactID) 
THEN 'Vendor Contact'
WHEN EXISTS(SELECT * FROM [Sales].[StoreContact] sc 
INNER JOIN [Person].[ContactType] ct 
ON sc.[ContactTypeID] = ct.[ContactTypeID] 
WHERE sc.[ContactID] = @ContactID) 
THEN 'Store Contact'
WHEN EXISTS(SELECT * FROM [Sales].[Individual] i 
WHERE i.[ContactID] = @ContactID) 
THEN 'Consumer'
END;
IF @ContactID IS NOT NULL 
BEGIN
INSERT @retContactInformation
SELECT @ContactID, @FirstName, @LastName, @JobTitle, @ContactType;
END;
RETURN;
END;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Input parameter for the table value function ufnGetContactInformation. Enter a valid ContactID from the Person.Contact table.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ufnGetContactInformation', @level2type = N'PARAMETER', @level2name = N'@ContactID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Table value function returning the first name, last name, job title and contact type for a given contact.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ufnGetContactInformation';

