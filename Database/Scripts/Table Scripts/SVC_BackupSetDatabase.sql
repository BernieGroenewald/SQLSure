USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_BackupSetDatabase]    Script Date: 2017/02/14 07:52:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_BackupSetDatabase](
	[BackupSetID] [int] NOT NULL,
	[DatabaseName] [varchar](255) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


