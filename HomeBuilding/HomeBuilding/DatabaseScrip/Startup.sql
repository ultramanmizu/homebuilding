/****** Object:  Table [dbo].[ConstructionContract]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstructionContract](
	[Id] [uniqueidentifier] NOT NULL,
	[ContractNumber] [nvarchar](50) NOT NULL,
	[ContractDate] [datetime] NOT NULL,
	[MadeAt] [nvarchar](1000) NULL,
	[OwnerName] [nvarchar](250) NULL,
	[OwnerAddress] [nvarchar](1000) NULL,
	[OwnerTel] [nvarchar](50) NULL,
	[OwnerEmail] [nvarchar](50) NULL,
	[OwnerType] [nvarchar](50) NULL,
	[OwnerNumber] [nvarchar](50) NULL,
	[ContractorName] [nvarchar](250) NULL,
	[ContractorAddress] [nvarchar](1000) NULL,
	[ContractorTel] [nvarchar](50) NULL,
	[ContractorEmail] [nvarchar](50) NULL,
	[ContractorLicenseNumber] [nvarchar](50) NULL,
	[DescriptionOfWork] [nvarchar](2000) NULL,
	[WorkSite] [nvarchar](1000) NULL,
	[ContractValue] [decimal](18, 2) NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK__Construc__3214EC07B9ABB7B5] PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContractDescription]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractDescription](
	[Id] [uniqueidentifier] NOT NULL,
	[ConstructionContractId] [uniqueidentifier] NOT NULL,
	[LineNo] [int] NOT NULL,
	[Detail] [nvarchar](1000) NULL,
	[Quantity] [decimal](16, 2) NOT NULL,
	[Unit] [nvarchar](250) NULL,
	[UnitPrice] [decimal](16, 2) NOT NULL,
	[Total] [decimal](16, 2) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MasterData]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MasterData](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](250) NOT NULL,
	[Text] [nvarchar](250) NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[ParentLevel] [int] NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](250) NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialUnit]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialUnit](
	[Id] [uniqueidentifier] NOT NULL,
	[MaterialId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receipt]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receipt](
	[Id] [uniqueidentifier] NOT NULL,
	[ConstructionContractId] [uniqueidentifier] NOT NULL,
	[Round] [int] NULL,
	[ReceiptNumber] [nvarchar](50) NULL,
	[ReceiptDate] [datetime] NULL,
	[Total] [decimal](16, 2) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK__Receipt__3214EC078841CA2C] PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptDetail]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[ReceiptId] [uniqueidentifier] NOT NULL,
	[LineNo] [int] NOT NULL,
	[Detail] [nvarchar](1000) NULL,
	[Percent] [decimal](16, 2) NOT NULL,
	[Total] [decimal](16, 2) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RunningNumber]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RunningNumber](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [nvarchar](250) NOT NULL,
	[Prefix] [nvarchar](50) NOT NULL,
	[StartNumber] [int] NOT NULL,
	[EndNumber] [int] NOT NULL,
	[CurrentNumber] [int] NOT NULL,
	[Digit] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitPrice]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitPrice](
	[Id] [uniqueidentifier] NOT NULL,
	[UnitId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Price] [decimal](16, 2) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[FirstName] [nvarchar](250) NULL,
	[LastName] [nvarchar](250) NULL,
	[FullName] [nvarchar](250) NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[LastLogIn] [datetime] NULL,
	[LastChangePassword] [datetime] NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Sequence] [int] NOT NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[DeletedById] [uniqueidentifier] NULL,
	[DeletedDate] [datetime] NULL,
PRIMARY Key CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_Key = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_Key = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ConstructionContract] ADD  CONSTRAINT [DF__Construction__Id__7A672E12]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ConstructionContract] ADD  CONSTRAINT [DF__Construct__Seque__7B5B524B]  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[ConstructionContract] ADD  CONSTRAINT [DF__Construct__IsEna__7C4F7684]  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[ConstructionContract] ADD  CONSTRAINT [DF__Construct__IsDel__7D439ABD]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ConstructionContract] ADD  CONSTRAINT [DF__Construct__Creat__7E37BEF6]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ConstructionContract] ADD  CONSTRAINT [DF__Construct__Updat__7F2BE32F]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((1)) FOR [LineNo]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((0.00)) FOR [Quantity]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((0.00)) FOR [UnitPrice]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((0.00)) FOR [Total]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ContractDescription] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MasterData] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[MaterialUnit] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[MaterialUnit] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[MaterialUnit] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[MaterialUnit] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MaterialUnit] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MaterialUnit] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__Id__0E6E26BF]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__Total__0F624AF8]  DEFAULT ((0.00)) FOR [Total]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__Sequenc__10566F31]  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__IsEnabl__114A936A]  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__IsDelet__123EB7A3]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__Created__1332DBDC]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Receipt] ADD  CONSTRAINT [DF__Receipt__Updated__14270015]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT ((1)) FOR [LineNo]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT ((0.00)) FOR [Percent]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT ((0.00)) FOR [Total]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ReceiptDetail] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Role] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((1)) FOR [StartNumber]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((9999999)) FOR [EndNumber]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((1)) FOR [CurrentNumber]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((6)) FOR [Digit]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((1)) FOR [Priority]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT (getdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[RunningNumber] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT ((0.00)) FOR [Price]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT (getdate()) FOR [StartDate]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UnitPrice] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [LastChangePassword]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [Sequence]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_document_number]    Script Date: 22/07/04 9:34:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		warin kesorn
-- Create date: 25/06/2022
-- Description:	for auto generate document running number
-- =============================================
CREATE PROCEDURE [dbo].[usp_get_document_number]
	@Key nvarchar(50),
	@Prefix nvarchar(50),
	@Digit int 
AS
BEGIN
	
	Declare @number int , @document_number nvarchar(250);

	IF NOT EXISTS (SELECT * FROM RunningNumber WHERE [Key] = @Key AND Prefix = @Prefix AND IsEnabled = 1 AND IsDeleted = 0) 
	BEGIN
		INSERT INTO RunningNumber ([Key],[Prefix],[Digit],[CreatedById],[UpdatedById])
		VALUES (@Key, @Prefix, @Digit, '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000');
	END

	--Get Current Number
	SELECT @number = CurrentNumber 
	FROM RunningNumber 
	WHERE [Key] = @Key AND Prefix = @Prefix AND IsEnabled = 1 AND IsDeleted = 0;

	--Count Number
	UPDATE RunningNumber 
	SET CurrentNumber = @number+1 , UpdatedDate = GETDATE()
	WHERE [Key] = @Key AND Prefix = @Prefix AND IsEnabled = 1 AND IsDeleted = 0;

	--Generate document number
	SET @document_number = CONCAT(@Prefix,right('000000000000000000'+convert(varchar(20),@number),@Digit));

	--Return
	SELECT @document_number AS DocumentNumber ;
	
END
GO


--=================================== mockup data ===================================
---------------------------------------- Role ---------------------------------------- 
INSERT INTO Role (Id,Name,Sequence,CreatedById,UpdatedById) VALUES ('CEB20945-2757-4D6E-A148-6643F431800C','Admin',1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO Role (Id,Name,Sequence,CreatedById,UpdatedById) VALUES ('94721C79-F9F2-4CDD-A35C-4B14E9E65DA7','Super User',2,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO Role (Id,Name,Sequence,CreatedById,UpdatedById) VALUES ('0E777BD4-BCAD-456F-A4D2-E084E5D47062','User',3,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
-------------------------------------- End Role --------------------------------------
---------------------------------------- User ---------------------------------------- 
INSERT INTO [User] (Username,Password,Title,FirstName,LastName,FullName,RoleId,RoleName,Email,CreatedById,UpdatedById)
VALUES ('admin','password','','Administrator','','Administrator','CEB20945-2757-4D6E-A148-6643F431800C','Admin','admin@mail.com','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO [User] (Username,Password,Title,FirstName,LastName,FullName,RoleId,RoleName,Email,CreatedById,UpdatedById)
VALUES ('superuser','password','','Super User','','Super User','94721C79-F9F2-4CDD-A35C-4B14E9E65DA7','Super User','superuser@mail.com','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO [User] (Username,Password,Title,FirstName,LastName,FullName,RoleId,RoleName,Email,CreatedById,UpdatedById)
VALUES ('user','password','','User','','User','0E777BD4-BCAD-456F-A4D2-E084E5D47062','User','user@mail.com','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO [User] (Username,Password,Title,FirstName,LastName,FullName,RoleId,RoleName,Email,CreatedById,UpdatedById)
VALUES ('somchai','password','นาย','สมชาย','เข็มกลัด','นาย สมชาย เข็มกลัด','0E777BD4-BCAD-456F-A4D2-E084E5D47062','User','somchai@mail.com','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
-------------------------------------- End User --------------------------------------
-------------------------------------- Material--------------------------------------- 
INSERT INTO Material(Id,Type,Name,Sequence,CreatedById,UpdatedById)
VALUES ('BD341263-9150-4EFE-BEEB-6212C594366A','Labor','งานก่อผนัง',1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO Material(Id,Type,Name,Sequence,CreatedById,UpdatedById)
VALUES ('EE60BF05-BF47-4D7D-B05B-388932E28EA7','Labor','งานฉาบ',2,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO Material(Id,Type,Name,Sequence,CreatedById,UpdatedById)
VALUES ('B55A8D25-682A-417F-A1A9-4B913679CACE','Labor','ทำความสะอาด',3,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
------------------------------------ End Material-------------------------------------
---------------------------------------- Unit ---------------------------------------- 
INSERT INTO MaterialUnit (Id,MaterialId,Name,Sequence,CreatedById,UpdatedById)
VALUES ('AAAA75D5-CBD2-400A-9296-FF3790F79152','BD341263-9150-4EFE-BEEB-6212C594366A','ตารางเมตร',1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO MaterialUnit (Id,MaterialId,Name,Sequence,CreatedById,UpdatedById)
VALUES ('14BFA502-9FF3-4DE4-A64F-7A554EEAEB98','BD341263-9150-4EFE-BEEB-6212C594366A','ตารางวา',2,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MaterialUnit (Id,MaterialId,Name,Sequence,CreatedById,UpdatedById)
VALUES ('17A91BE0-FFAE-4B89-B778-628906F81CED','EE60BF05-BF47-4D7D-B05B-388932E28EA7','ตารางเมตร',1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO MaterialUnit (Id,MaterialId,Name,Sequence,CreatedById,UpdatedById)
VALUES ('51EAFAFE-DF41-4478-AA30-11E0A6E8757E','EE60BF05-BF47-4D7D-B05B-388932E28EA7','ตารางวา',2,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MaterialUnit (Id,MaterialId,Name,Sequence,CreatedById,UpdatedById)
VALUES ('D3CB338B-381A-4466-BA78-B922C7368E95','B55A8D25-682A-417F-A1A9-4B913679CACE','เหมาจ่าย',1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
INSERT INTO MaterialUnit (Id,MaterialId,Name,Sequence,CreatedById,UpdatedById)
VALUES ('162B8111-EED5-42ED-B254-E78E6D931868','B55A8D25-682A-417F-A1A9-4B913679CACE','รายวัน',2,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
------------------------------------- End Unit ---------------------------------------
------------------------------------- UnitPirce --------------------------------------
INSERT INTO UnitPirce (UnitId,Name,Price,Sequence,CreatedById,UpdatedById)
VALUES ('AAAA75D5-CBD2-400A-9296-FF3790F79152','350.00',350.00,1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000')
INSERT INTO UnitPirce (UnitId,Name,Price,Sequence,CreatedById,UpdatedById)
VALUES ('14BFA502-9FF3-4DE4-A64F-7A554EEAEB98','1,200.00',1200.00,1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000')

INSERT INTO UnitPirce (UnitId,Name,Price,Sequence,CreatedById,UpdatedById)
VALUES ('51EAFAFE-DF41-4478-AA30-11E0A6E8757E','2,000.00',2000.00,1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000')
INSERT INTO UnitPirce (UnitId,Name,Price,Sequence,CreatedById,UpdatedById)
VALUES ('17A91BE0-FFAE-4B89-B778-628906F81CED','500.00',500.00,1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000')

INSERT INTO UnitPirce (UnitId,Name,Price,Sequence,CreatedById,UpdatedById)
VALUES ('162B8111-EED5-42ED-B254-E78E6D931868','600.00',600.00,1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000')
INSERT INTO UnitPirce (UnitId,Name,Price,Sequence,CreatedById,UpdatedById)
VALUES ('D3CB338B-381A-4466-BA78-B922C7368E95','2,500.00',2500.00,1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000')
------------------------------------- End UnitPirce -------------------------------------
------------------------------------- MasterData ----------------------------------------
INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานก่อสร้าง 25%','งานก่อสร้าง 25%',1,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานก่อสร้าง 50%','งานก่อสร้าง 50%',2,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานก่อสร้าง 75%','งานก่อสร้าง 75%',3,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานก่อสร้าง 100%','งานก่อสร้าง 100%',4,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานฉาบ 25%','งานฉาบ 25%',5,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานฉาบ 50%','งานฉาบ 50%',6,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานฉาบ 75%','งานฉาบ 75%',7,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','งานฉาบ 100%','งานฉาบ 100%',8,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','ลงเสาเข็ม','ลงเสาเข็ม',9,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');

INSERT INTO MasterData([Key],Text,Value,Sequence,CreatedById,UpdatedById)
VALUES ('withdraw','ทำความสะอาด','ทำความสะอาด',10,'00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000');
------------------------------------- End MasterData ----------------------------------------