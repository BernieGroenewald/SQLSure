USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_ProjectObjectHistory]    Script Date: 2017/02/14 07:56:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_ProjectObjectHistory](
	[ProjectObjectHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectObjectID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[FileObjectID] [int] NULL,
	[DBName] [varchar](255) NULL,
	[DBObjectName] [varchar](255) NULL,
	[ObjectSequence] [int] NULL,
	[ObjectSelected] [varchar](1) NULL,
	[DateModified] [datetime] NULL,
	[UserID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


