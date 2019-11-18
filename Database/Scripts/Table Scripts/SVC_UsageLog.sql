USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_UsageLog]    Script Date: 2017/02/14 07:59:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_UsageLog](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[QTNumber] [varchar](10) NOT NULL,
	[CallingForm] [varchar](50) NULL,
	[Logdate] [datetime] NULL,
 CONSTRAINT [PK_SVC_UsageLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


