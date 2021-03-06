SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

CREATE TABLE [dbo].[Expense](
  [Id] [uniqueidentifier] NOT NULL,
  [UserName] [nvarchar](1024) NOT NULL,
  [Title] [nvarchar](30) NOT NULL,
  [Description] [nvarchar](100) NOT NULL,
  [Amount] [money] NOT NULL,
  [Date] [datetime] NOT NULL,
  [Approved] [bit] NOT NULL,
  [CostCenter] [nvarchar](50) NOT NULL,
  [ReimbursementMethod] [nvarchar](50) NOT NULL,
  [Approver] [nvarchar](1024) NOT NULL,
  CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED ([Id] ASC)
)
            
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF_Expense_Id]  DEFAULT (newid()) FOR [Id]
ALTER TABLE [dbo].[Expense] ADD  CONSTRAINT [DF_Expense_Approved]  DEFAULT ((0)) FOR [Approved]

CREATE TABLE [dbo].[ExpenseDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](1024) NOT NULL,
	[Amount] [money] NOT NULL,
	[ExpenseId] [uniqueidentifier] NOT NULL,
	[ReceiptThumbnailUrl] [nvarchar](max) NULL,
	[ReceiptUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_ExpenseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
)

/****** Object:  Default [DF_ExpenseDetail_Id]    Script Date: 07/27/2012 12:04:25 ******/
ALTER TABLE [dbo].[ExpenseDetail] ADD  CONSTRAINT [DF_ExpenseDetail_Id]  DEFAULT (newid()) FOR [Id]

/****** Object:  ForeignKey [FK_ExpenseDetail_Expense]    Script Date: 07/27/2012 12:04:25 ******/
ALTER TABLE [dbo].[ExpenseDetail]  WITH CHECK ADD  CONSTRAINT [FK_ExpenseDetail_Expense] FOREIGN KEY([ExpenseId])
REFERENCES [dbo].[Expense] ([Id])

ALTER TABLE [dbo].[ExpenseDetail] CHECK CONSTRAINT [FK_ExpenseDetail_Expense]


/****** Inserts Sample Data ******/
INSERT INTO [dbo].[Expense]([Id], [UserName], [Title], [Description], [Amount], [Date], [Approved], [CostCenter], [ReimbursementMethod], [Approver])
  VALUES(N'abafc874-d0cc-4245-9319-1e5a75108a41', N'ADATUM\mary', N'Dinner', N'Dinner with new employee', 125.2500, '2009-08-25', 0, N'CC-12345-DEV', N'Check', N'ADATUM\bob')

INSERT INTO [dbo].[Expense]([Id], [UserName], [Title], [Description], [Amount], [Date], [Approved], [CostCenter], [ReimbursementMethod], [Approver])
  VALUES(N'abafc874-d0cc-4245-9319-1e5c77158b42', N'ADATUM\johndoe', N'Dinner', N'Dinner with customer', 200.5000, '2009-08-25', 0, N'CC-12345-DEV', N'Check', N'ADATUM\mary')

INSERT INTO [dbo].[Expense]([Id], [UserName], [Title], [Description], [Amount], [Date], [Approved], [CostCenter], [ReimbursementMethod], [Approver])
  VALUES(N'abafc874-a0cc-4145-9319-1e5c78508a41', N'ADATUM\johndoe', N'Breakfast', N'Breakfast with the team', 75.9000, '2009-08-23', 1, N'CC-12345-DEV', N'Check', N'ADATUM\mary')
  
  /****** Object:  Table [dbo].[ExpenseDetail]    Script Date: 08/02/2012 18:05:57 ******/
INSERT [dbo].[ExpenseDetail] ([Id], [Description], [Amount], [ExpenseId], [ReceiptThumbnailUrl], [ReceiptUrl]) VALUES (N'abafc874-d0cc-4245-9319-1e5a75108a00', N'Dinner', 125.2500, N'abafc874-d0cc-4245-9319-1e5a75108a41', NULL, NULL)
INSERT [dbo].[ExpenseDetail] ([Id], [Description], [Amount], [ExpenseId], [ReceiptThumbnailUrl], [ReceiptUrl]) VALUES (N'abafc874-d0cc-4245-9319-1e5a75108a01', N'Dinner', 200.5000, N'abafc874-d0cc-4245-9319-1e5c77158b42', NULL, NULL)
INSERT [dbo].[ExpenseDetail] ([Id], [Description], [Amount], [ExpenseId], [ReceiptThumbnailUrl], [ReceiptUrl]) VALUES (N'abafc874-d0cc-4245-9319-1e5a75108a02', N'Breakfast', 75.9000, N'abafc874-a0cc-4145-9319-1e5c78508a41', NULL, NULL)

  
  



