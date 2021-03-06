/****** Object:  Table [dbo].[Cart] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cart]') AND type in (N'U'))
DROP TABLE [dbo].[Cart]

/****** Object:  Table [dbo].[OrderStatus] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderStatus]') AND type in (N'U'))
DROP TABLE [dbo].[OrderStatus]

/****** Object:  Table [dbo].[OrderProcessStatus] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderProcessStatus]') AND type in (N'U'))
DROP TABLE [dbo].[OrderProcessStatus]

/****** Object:  Table [dbo].[OrderDetail] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetail]') AND type in (N'U'))
DROP TABLE [dbo].[OrderDetail]

/****** Object:  Table [dbo].[Order] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Order]') AND type in (N'U'))
DROP TABLE [dbo].[Order]

/****** Object:  Table [dbo].[Product] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]

/****** Object:  Table [dbo].[Customer] ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]

/****** Object:  Table [dbo].[Cart] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Cart](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[CartId] [nvarchar](max) NOT NULL,
	[Count] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([RecordId] ASC)
 )
 
 
/****** Object:  Table [dbo].[OrderStatus] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[OrderStatus](
	[OrderId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[Status] ASC
)
) 
  
/****** Object:  Table [dbo].[OrderDetail] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED ( [OrderDetailId] ASC )
) 

ALTER TABLE [dbo].[OrderDetail] ADD  CONSTRAINT [DF_OrderDetail_OrderDetailId]  DEFAULT (newid()) FOR [OrderDetailId]

/****** Object:  Table [dbo].[Order] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Order](
	[OrderId] [uniqueidentifier] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Total] [numeric](10, 2) NOT NULL,
	[Address] [nvarchar](256) NOT NULL,
	[City] [nvarchar](256) NOT NULL,
	[State] [nvarchar](256) NOT NULL,
	[PostalCode] [nvarchar](256) NOT NULL,
	[Country] [nvarchar](256) NOT NULL,
	[Phone] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[TransportPartner] [nvarchar](256) NULL,
	[TrackingId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_OrderId] PRIMARY KEY CLUSTERED ( [OrderId] ASC )
) 

/****** Object:  Table [dbo].[Product] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Price] [numeric](10, 2) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_ProductId] PRIMARY KEY CLUSTERED (	[ProductId] ASC )
)

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
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
 )
 
 ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_CustomerId]  DEFAULT (newid()) FOR [CustomerId]

/****** Object:  Table [dbo].[OrderProcessStatus] ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[OrderProcessStatus](
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProcessStatus] [varchar](50) NOT NULL,
	[LockedBy] [nvarchar](255) NULL,
	[LockedUntil] [datetime] NULL,
	[Version] [timestamp] NULL,
	[BatchId] [uniqueidentifier] NULL,
   	[RetryCount] [int] NOT NULL,
 CONSTRAINT [PK_OrderProcessStatus] PRIMARY KEY CLUSTERED (	[OrderId] ASC)
)

/****** Object:  ForeignKey [FK_OrderDetail_Order] ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])

ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]

/****** Object:  ForeignKey [FK_OrderProcessStatus_Order] ******/
ALTER TABLE [dbo].[OrderProcessStatus]  WITH CHECK ADD  CONSTRAINT [FK_OrderProcessStatus_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])

ALTER TABLE [dbo].[OrderProcessStatus] CHECK CONSTRAINT [FK_OrderProcessStatus_Order]

/****** Object:  ForeignKey [FK_OrderDetail_Product] ******/
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])

ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]

/****** Object:  ForeignKey [FK_Cart_Product]******/
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])

ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Product]

/****** Object:  ForeignKey [FK_OrderStatus_Order] ******/
ALTER TABLE [dbo].[OrderStatus]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatus_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])

ALTER TABLE [dbo].[OrderStatus] CHECK CONSTRAINT [FK_OrderStatus_Order]

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

