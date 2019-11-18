USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_SystemLog]    Script Date: 2017/02/14 07:57:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_SystemLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [varchar](50) NOT NULL,
	[LogDateTime] [datetime] NULL,
	[LogDescription] [varchar](1024) NULL,
	[UserName] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


