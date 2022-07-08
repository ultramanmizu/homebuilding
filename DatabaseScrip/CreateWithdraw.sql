CREATE TABLE [dbo].[Withdraw](
	[Id] [uniqueidentifier] NOT NULL,
	[ConstructionContractId] [uniqueidentifier] NOT NULL,
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT ((1)) FOR [LineNo]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT ((0.00)) FOR [Percent]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT ((0.00)) FOR [Total]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT ((0)) FOR [Sequence]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT ((1)) FOR [IsEnabled]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Withdraw] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
