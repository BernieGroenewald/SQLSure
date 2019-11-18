USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_BackupSetObjectType]    Script Date: 2017/02/14 07:53:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_BackupSetObjectType](
	[BackupSetID] [int] NOT NULL,
	[ObjectType] [varchar](255) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


