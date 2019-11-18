USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_Server]    Script Date: 2017/02/14 07:56:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_Server](
	[ServerID] [int] IDENTITY(1,1) NOT NULL,
	[ServerAliasID] [int] NOT NULL,
	[ServerName] [varchar](250) NOT NULL,
	[DBOwner] [varchar](50) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varbinary](256) NULL,
	[ServerActive] [varchar](1) NULL,
	[IntegratedSecurity] [varchar](1) NULL,
	[CreateDate] [datetime] NULL,
	[ServerRoleID] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_Server] PRIMARY KEY CLUSTERED 
(
	[ServerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


