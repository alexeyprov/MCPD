CREATE VIEW [Purchasing].[vVendor] AS 
SELECT 
v.[VendorID]
,v.[Name]
,ct.[Name] AS [ContactType]
,c.[Title]
,c.[FirstName]
,c.[MiddleName]
,c.[LastName]
,c.[Suffix]
,c.[Phone]
,c.[EmailAddress]
,c.[EmailPromotion]
,a.[AddressLine1]
,a.[AddressLine2]
,a.[City]
,[StateProvinceName] = sp.[Name]
,a.[PostalCode]
,[CountryRegionName] = cr.[Name]
FROM [Purchasing].[Vendor] v
INNER JOIN [Purchasing].[VendorContact] vc 
ON vc.[VendorID] = v.[VendorID]
INNER JOIN [Person].[Contact] c 
ON c.[ContactID] = vc.[ContactID]
INNER JOIN [Person].[ContactType] ct 
ON vc.[ContactTypeID] = ct.[ContactTypeID]
INNER JOIN [dbo].[CounterpartyAddress] va 
ON va.[CounterpartyID] = v.[VendorID]
INNER JOIN [Person].[Address] a 
ON a.[AddressID] = va.[AddressID]
INNER JOIN [Person].[StateProvince] sp 
ON sp.[StateProvinceID] = a.[StateProvinceID]
INNER JOIN [Person].[CountryRegion] cr 
ON cr.[CountryRegionCode] = sp.[CountryRegionCode];
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vendor (company) names and addresses and the names of vendor employees to contact.', @level0type = N'SCHEMA', @level0name = N'Purchasing', @level1type = N'VIEW', @level1name = N'vVendor';

