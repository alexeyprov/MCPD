function Get-DatabaseDeleteSql($DatabaseName)
{
    return "DROP DATABASE [$DatabaseName]";
}

function Get-DatabaseCreateSql($DatabaseName)
{
    return "CREATE DATABASE [$DatabaseName]";
}

function Get-DatabaseSetupSql($DatabaseName)
{
    return "
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
    	[Approver] [nvarchar](1024) NOT NULL
     CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
    (
    	[Id] ASC
    )WITH (STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF)
    )

    ALTER TABLE [Expense] ADD  CONSTRAINT [DF_Expense_Id]  DEFAULT (newid()) FOR [Id]
    ALTER TABLE [Expense] ADD  CONSTRAINT [DF_Expense_Approved]  DEFAULT ((0)) FOR [Approved]
    /****** Create tables and setup database - End ******/
	
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


    /****** Populate database with sample data - Start ******/
    USE [$NewDatabaseName]

    INSERT INTO [dbo].[Expense] 
		([Id], [UserName], [Title], [Description], [Amount], [Date], [Approved], [CostCenter], [ReimbursementMethod], [Approver]) 
	VALUES 
		(N'abafc874-d0cc-4245-9319-1e5a75108a41', N'ADATUM\mary', N'Dinner', N'Dinner with new employee', 125.2500, '2009-08-25', 0, N'CC-12345-DEV', N'Check', N'ADATUM\bob')
		
    INSERT INTO [dbo].[Expense] 
		([Id], [UserName], [Title], [Description], [Amount], [Date], [Approved], [CostCenter], [ReimbursementMethod], [Approver]) 
	VALUES 
		(N'abafc874-d0cc-4245-9319-1e5c77158b42', N'ADATUM\johndoe', N'Dinner', N'Dinner with customer', 200.5000, '2009-08-25', 0, N'CC-12345-DEV', N'Check', N'ADATUM\mary')
		
    INSERT INTO [dbo].[Expense] 
		([Id], [UserName], [Title], [Description], [Amount], [Date], [Approved], [CostCenter], [ReimbursementMethod], [Approver]) 
	VALUES 
		(N'abafc874-a0cc-4145-9319-1e5c78508a41', N'ADATUM\johndoe', N'Breakfast', N'Breakfast with the team', 75.9000, '2009-08-23', 1, N'CC-12345-DEV', N'Check', N'ADATUM\mary')
    /****** Populate database with sample data - End ******/";
}


function Get-DatabaseSetupAspnetSql($DatabaseName)
{
    return "
	    USE [$NewDatabaseName]

		INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'aExpense', N'aexpense', N'4450c5b0-924e-4de8-ba94-0ece7434e80c', NULL)
		INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'016d7f30-181c-4e97-9447-23374e459427', N'ADATUM\johndoe', N'adatum\johndoe', NULL, 0, CAST(0x00009D34011FBB8D AS DateTime))
		INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'763a6b6b-ee08-40e9-8456-9e01708cf63d', N'ADATUM\mary', N'adatum\mary', NULL, 0, CAST(0x00009D3401204036 AS DateTime))
		INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'363a6b6b-ee08-40e9-8456-9e01708cf63d', N'ADATUM\bob', N'adatum\bob', NULL, 0, CAST(0x00009D3401204036 AS DateTime))
		
		/* INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'c6235664-3fff-4fd2-a9bf-51efb741ac45', N'Employee', N'employee', NULL)
		INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'e5a7ccdb-3240-44d7-8ce9-8cf9f971704b', N'Manager', N'manager', NULL)
		INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'016d7f30-181c-4e97-9447-23374e459427', N'c6235664-3fff-4fd2-a9bf-51efb741ac45')
		INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'763a6b6b-ee08-40e9-8456-9e01708cf63d', N'c6235664-3fff-4fd2-a9bf-51efb741ac45')
		INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'763a6b6b-ee08-40e9-8456-9e01708cf63d', N'e5a7ccdb-3240-44d7-8ce9-8cf9f971704b')
		INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'363a6b6b-ee08-40e9-8456-9e01708cf63d', N'c6235664-3fff-4fd2-a9bf-51efb741ac45')
		INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'363a6b6b-ee08-40e9-8456-9e01708cf63d', N'e5a7ccdb-3240-44d7-8ce9-8cf9f971704b')
		INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'016d7f30-181c-4e97-9447-23374e459427', N'96l4oPFilscHFmvrjakgxl+LwTs=', 1, N'557MuvKetj4vy/dxBvwdrg==', NULL, N'johndoe@adatum.com', N'johndoe@adatum.com', N'pet', N'w7jMPgwv3lFtyU74OK9l4ytX5lc=', 1, 0, CAST(0x00009D34011FBB60 AS DateTime), CAST(0x00009D34011FBB8D AS DateTime), CAST(0x00009D34011FBB60 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
		INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'763a6b6b-ee08-40e9-8456-9e01708cf63d', N'LyOEcDUYP6CztkUvOK/Jgw3inxA=', 1, N'uKhcnfN++3GG4ffaoQLPbg==', NULL, N'mary@adatum.com', N'mary@adatum.com', N'pet', N'M/khtJ2fjHAyPB/spfmfyHpXZrc=', 1, 0, CAST(0x00009D3401203FCC AS DateTime), CAST(0x00009D3401204036 AS DateTime), CAST(0x00009D3401203FCC AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
		INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4450c5b0-924e-4de8-ba94-0ece7434e80c', N'363a6b6b-ee08-40e9-8456-9e01708cf63d', N'LyOEcDUYP6CztkUvOK/Jgw3inxA=', 1, N'uKhcnfN++3GG4ffaoQLPbg==', NULL, N'bob@adatum.com', N'bob@adatum.com', N'pet', N'M/khtJ2fjHAyPB/spfmfyHpXZrc=', 1, 0, CAST(0x00009D3401203FCC AS DateTime), CAST(0x00009D3401204036 AS DateTime), CAST(0x00009D3401203FCC AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
		*/

		/* If you change values below be aware: DisplayName:S:0:8 is saying that the display name will go from 0 to 8 */ 
		INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'016d7f30-181c-4e97-9447-23374e459427', N'PreferredReimbursementMethod:S:0:4:', N'Cash', 0x, CAST(0x00009D350046F628 AS DateTime))
		INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'763a6b6b-ee08-40e9-8456-9e01708cf63d', N'PreferredReimbursementMethod:S:0:5:', N'Check', 0x, CAST(0x00009D350046F628 AS DateTime))
		INSERT [dbo].[aspnet_Profile] ([UserId], [PropertyNames], [PropertyValuesString], [PropertyValuesBinary], [LastUpdatedDate]) VALUES (N'363a6b6b-ee08-40e9-8456-9e01708cf63d', N'PreferredReimbursementMethod:S:0:5:', N'Check', 0x, CAST(0x00009D350046F628 AS DateTime))
	";
}
