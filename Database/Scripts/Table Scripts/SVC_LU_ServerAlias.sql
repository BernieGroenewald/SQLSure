USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_LU_ServerAlias]    Script Date: 2017/02/14 07:54:12 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_LU_ServerAlias](
	[ServerAliasID] [int] IDENTITY(1,1) NOT NULL,
	[ServerAliasDesc] [varchar](50) NOT NULL,
	[ReleaseOrder] [tinyint] NULL,
	[ServerGroupID] [int] NULL,
 CONSTRAINT [PK_LU_ServerAlias] PRIMARY KEY CLUSTERED 
(
	[ServerAliasID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


