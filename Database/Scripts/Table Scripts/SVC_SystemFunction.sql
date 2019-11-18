USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_SystemFunction]    Script Date: 2017/02/14 07:57:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_SystemFunction](
	[SystemFunctionId] [int] IDENTITY(1,1) NOT NULL,
	[FunctionName] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


