USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_FileObject]    Script Date: 2017/02/14 07:53:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_FileObject](
	[FileObjectID] [int] IDENTITY(1,1) NOT NULL,
	[FileObject] [varchar](max) NOT NULL,
	[ObjectDescription] [varchar](255) NULL,
 CONSTRAINT [PK_SVC_FileObject] PRIMARY KEY CLUSTERED 
(
	[FileObjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


