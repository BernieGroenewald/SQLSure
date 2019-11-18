USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_ProjectObject]    Script Date: 2017/02/14 07:56:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_ProjectObject](
	[ProjectObjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[FileObjectID] [int] NULL,
	[DBName] [varchar](255) NULL,
	[DBObjectName] [varchar](255) NULL,
	[ObjectSequence] [int] NULL,
	[ObjectSelected] [varchar](1) NULL,
	[DateModified] [datetime] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_SVC_ProjectObject] PRIMARY KEY CLUSTERED 
(
	[ProjectObjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


