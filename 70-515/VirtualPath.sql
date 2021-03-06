/*
   Saturday, January 29, 20115:58:17 PM
   User: sa
   Server: EPUSPRIW0081
   Database: VirtualPath
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.AspContent
	(
	FileName varchar(50) NOT NULL,
	[Content] ntext NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.AspContent SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
