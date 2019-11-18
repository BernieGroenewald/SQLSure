USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_BackupSetObject]    Script Date: 2017/02/14 07:52:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_BackupSetObject](
	[BackupSetID] [int] NOT NULL,
	[HistoryID] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ObjectType] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


