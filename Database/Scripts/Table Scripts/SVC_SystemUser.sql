USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_SystemUser]    Script Date: 2017/02/14 07:58:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_SystemUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[QTNumber] [varchar](10) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[Active] [varchar](1) NULL,
	[CreateDate] [datetime] NULL,
	[EmailAddress] [varchar](100) NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


