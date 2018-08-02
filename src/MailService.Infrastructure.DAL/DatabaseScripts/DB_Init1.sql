USE [MailService]
GO
/****** Object:  Schema [mail]    Script Date: 8/3/2018 12:49:55 AM ******/
CREATE SCHEMA [mail]
GO
/****** Object:  Schema [msg]    Script Date: 8/3/2018 12:49:56 AM ******/
CREATE SCHEMA [msg]
GO
/****** Object:  Schema [pbl]    Script Date: 8/3/2018 12:49:56 AM ******/
CREATE SCHEMA [pbl]
GO
/****** File Groups *****/
ALTER DATABASE MailService
ADD FILEGROUP fgCLOB
GO

ALTER DATABASE MailService
ADD FILEGROUP fgData
GO

ALTER DATABASE MailService
ADD FILEGROUP fgIndexes
GO

/*****mama ********/
ALTER DATABASE MailService
ADD FILE (
	NAME = MailService_CLOB
	, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MailService_CLOB.ndf'
	, SIZE = 6MB
	, MAXSIZE = Unlimited 
	, FILEGROWTH = 1
)
TO FILEGROUP fgCLOB
GO

ALTER DATABASE MailService
ADD FILE (
	NAME = MailService_Data
	, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MailService_Data.ndf'
	, SIZE = 6MB
	, MAXSIZE = Unlimited 
	, FILEGROWTH = 1
)
TO FILEGROUP fgData
GO

ALTER DATABASE MailService
ADD FILE (
	NAME = MailService_Indexes
	, FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MailService_Indexes.ndf'
	, SIZE = 6MB
	, MAXSIZE = Unlimited 
	, FILEGROWTH = 1
)
TO FILEGROUP fgIndexes	
GO

/****** Object:  Table [mail].[Mail]    Script Date: 8/3/2018 12:49:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mail].[Mail](
	[ID] [uniqueidentifier] NOT NULL,
	[SourceAccountID] [uniqueidentifier] NOT NULL,
	[Priority] [tinyint] NOT NULL,
	[SendType] [tinyint] NOT NULL,
	[EncodingType] [tinyint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](500) NULL,
	[IsQueue] [bit] NULL,
	[QueueDate] [datetime] NULL,
	[IsSent] [bit] NULL,
	[SendDate] [datetime] NULL,
 CONSTRAINT [PK_Outgoing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData] TEXTIMAGE_ON [fgCLOB]
GO
/****** Object:  Table [mail].[MailReceiver]    Script Date: 8/3/2018 12:49:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mail].[MailReceiver](
	[ID] [uniqueidentifier] NOT NULL,
	[MailID] [uniqueidentifier] NOT NULL,
	[Email] [varchar](256) NULL,
	[Cc] [varchar](256) NULL,
	[Bcc] [varchar](256) NULL,
 CONSTRAINT [PK_CellNumber] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [mail].[SendTry]    Script Date: 8/3/2018 12:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mail].[SendTry](
	[ID] [uniqueidentifier] NOT NULL,
	[MailID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Succeed] [bit] NOT NULL,
	[Message] [nvarchar](500) NULL,
 CONSTRAINT [PK_SendTry] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [pbl].[Account]    Script Date: 8/3/2018 12:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Account](
	[ID] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[SSL] [bit] NULL,
	[Port] [int] NULL,
	[Host] [nvarchar](50) NULL,
	[Type] [tinyint] NULL,
	[IsRemoved] [bit] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO
/****** Object:  Table [pbl].[Config]    Script Date: 8/3/2018 12:49:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [pbl].[Config](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Value] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
) ON [fgData]
GO

/****** Object:  Index [UQ__Config__737584F6A0575502]    Script Date: 8/3/2018 12:50:00 AM ******/
ALTER TABLE [pbl].[Config] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [fgData]
GO
ALTER TABLE [mail].[Mail] ADD  CONSTRAINT [DF__Outgoing__Status__286302EC]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [mail].[Mail]  WITH CHECK ADD  CONSTRAINT [FK_Outgoing_Account] FOREIGN KEY([SourceAccountID])
REFERENCES [pbl].[Account] ([ID])
GO
ALTER TABLE [mail].[Mail] CHECK CONSTRAINT [FK_Outgoing_Account]
GO
ALTER TABLE [mail].[MailReceiver]  WITH CHECK ADD  CONSTRAINT [FK_ReceiverMessage_Message] FOREIGN KEY([MailID])
REFERENCES [mail].[Mail] ([ID])
GO
ALTER TABLE [mail].[MailReceiver] CHECK CONSTRAINT [FK_ReceiverMessage_Message]
GO
ALTER TABLE [mail].[SendTry]  WITH CHECK ADD  CONSTRAINT [FK_SendTry_Mail] FOREIGN KEY([MailID])
REFERENCES [mail].[Mail] ([ID])
GO
ALTER TABLE [mail].[SendTry] CHECK CONSTRAINT [FK_SendTry_Mail]
GO