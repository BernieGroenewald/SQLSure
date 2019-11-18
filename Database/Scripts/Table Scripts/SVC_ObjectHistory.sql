USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_ObjectHistory]    Script Date: 2017/02/14 07:55:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_ObjectHistory](
	[HistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ServerID] [int] NOT NULL,
	[HistoryDate] [datetime] NOT NULL,
	[ObjectStatusID] [int] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[ObjectName] [varchar](255) NOT NULL,
	[DatabaseName] [varchar](255) NOT NULL,
	[ObjectText] [varchar](max) NOT NULL,
	[Comment] [varchar](2048) NULL,
	[ProjectID] [int] NULL,
	[ObjectVersion] [int] NULL,
	[VersionAtCheckOut] [varchar](1) NULL,
 CONSTRAINT [PK_ObjectHistory] PRIMARY KEY CLUSTERED 
(
	[HistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


