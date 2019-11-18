USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_BackupSet]    Script Date: 2017/02/14 07:51:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_BackupSet](
	[BackupSetID] [int] IDENTITY(1,1) NOT NULL,
	[BackupSetName] [varchar](50) NOT NULL,
	[BackupSetDesc] [varchar](1024) NULL,
	[UserID] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Active] [varchar](1) NULL,
	[ServerAliasID] [int] NULL,
 CONSTRAINT [PK_BackupSet] PRIMARY KEY CLUSTERED 
(
	[BackupSetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


