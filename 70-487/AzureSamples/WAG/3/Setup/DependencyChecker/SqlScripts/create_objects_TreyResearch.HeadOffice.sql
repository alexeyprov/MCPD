/****** Object:  Table [dbo].[AuditLog] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLog]') AND type in (N'U'))
DROP TABLE [dbo].[AuditLog]

/****** Object:  Table [dbo].[AuditLog] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[AuditLog](
	[OrderId] [uniqueidentifier] NOT NULL,
	[Amount] [money] NOT NULL,
	[CustomerName] [nvarchar](255) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Customer] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]

/****** Object:  Table [dbo].[Customer] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Customer](
	[CustomerId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[PostalCode] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CustomerId]  DEFAULT (newid()) FOR [CustomerId]

/****** Object:  Table [dbo].[Product]  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]

/****** Object:  Table [dbo].[Product]  ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Price] [numeric](10, 2) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ProductId] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[DiagnosticsLog] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DiagnosticsLog]') AND type in (N'U'))
DROP TABLE [dbo].[DiagnosticsLog]

/****** Object:  Table [dbo].[DiagnosticsLog] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[DiagnosticsLog](
	[Id] [uniqueidentifier] NOT NULL,
	[PartitionKey] [nvarchar](250) NOT NULL,
	[RowKey] [nvarchar](250) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[DataCenter] [nvarchar](50) NOT NULL,
	[DeploymentId] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
	[RoleInstance] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](MAX) NOT NULL,
 CONSTRAINT [PK_DiagnosticsLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

/****** Inserts Sample Data ******/
SET IDENTITY_INSERT [dbo].[Product] ON

INSERT INTO Product (ProductId, Description, Price, Active) VALUES (386,'Titanium Extension Bracket Left Hand',CAST(7000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (387,'Titanium Extension Bracket Right Hand',CAST(5000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (388,'Fusion Generator Module 5 kV',CAST(15000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (389,'Bypass Filter 400 MHz Low Pass',CAST(500 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (390,'Bypass Filter 800 MHz High Pass',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (391,'Carbon Nanotube 18 x 200',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (392,'Carbon Nanotube 24 x 600',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (393,'High Capacity Particle Mask',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (394,'Wave Guide Carrier Type 7',CAST(2000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (395,'Twin Oscillating Wave Guide',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (396,'Cycloalutanium Tube 10 x 2000',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (397,'Connector for Cycloalutanium Tube',CAST(1500 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (398,'Bracket for Cycloalutanium Tube',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (399,'Alternating Neutron Accelerator',CAST(15000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (400,'Low Mass Directional Coil Type 175FG',CAST(1500 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (401,'High Mass Directional Coil Type 176HS',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (402,'High Tensile Steel Bolt  R2 x 16 RHT',CAST(1000 AS Numeric(10, 2)), 1)
INSERT INTO Product (ProductId, Description, Price, Active) VALUES (403,'High Tensile Steel Bolt  R2 x 16 LHT',CAST(500 AS Numeric(10, 2)), 1)

SET IDENTITY_INSERT [dbo].[Product] OFF