USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_NotifyLog]    Script Date: 2017/02/14 07:55:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_NotifyLog](
	[NotifyLogID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[ObjectName] [varchar](255) NULL,
	[DBName] [varchar](50) NULL,
	[LogMessage] [varchar](4096) NULL,
	[LogDate] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


