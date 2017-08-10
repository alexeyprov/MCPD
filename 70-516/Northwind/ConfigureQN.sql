USE [Northwind]
GO

SELECT * FROM sys.dm_qn_subscriptions where sid = SUSER_SID('hanauma\Alexey')
select * from sys.objects where object_id = 530100929
select * from sys.internal_tables where object_id = 530100929

exec sp_changedbowner 'hanauma\Alexey'

ALTER DATABASE [Northwind] SET ANSI_NULLS ON WITH NO_WAIT
GO
ALTER DATABASE [Northwind] SET ANSI_PADDING ON WITH NO_WAIT
GO
ALTER DATABASE [Northwind] SET ANSI_WARNINGS ON WITH NO_WAIT
GO
ALTER DATABASE [Northwind] SET ARITHABORT ON WITH NO_WAIT
GO
ALTER DATABASE [Northwind] SET CONCAT_NULL_YIELDS_NULL ON WITH NO_WAIT
GO
ALTER DATABASE [Northwind] SET QUOTED_IDENTIFIER ON WITH NO_WAIT
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD='Northwind'