IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AdventureWorksUser')
DROP USER [AdventureWorksUser]
GO
IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'IIS APPPOOL\DefaultAppPool')
DROP LOGIN [IIS APPPOOL\DefaultAppPool]
GO
CREATE LOGIN [IIS APPPOOL\DefaultAppPool] 
  FROM WINDOWS WITH DEFAULT_DATABASE=[master], 
  DEFAULT_LANGUAGE=[us_english]
GO
CREATE USER [AdventureWorksUser] 
  FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO
EXEC sp_addrolemember 'db_datareader', 'AdventureWorksUser'
GO
EXEC sp_addrolemember 'aspnet_Membership_BasicAccess', 'AdventureWorksUser'
GO
EXEC sp_addrolemember 'aspnet_Profile_BasicAccess', 'AdventureWorksUser'
GO
EXEC sp_addrolemember 'aspnet_Roles_BasicAccess', 'AdventureWorksUser'
GO
GRANT EXECUTE TO [AdventureWorksUser]
GO