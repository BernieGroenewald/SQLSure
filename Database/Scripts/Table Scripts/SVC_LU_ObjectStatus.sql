USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_LU_ObjectStatus]    Script Date: 2017/02/14 07:53:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_LU_ObjectStatus](
	[ObjectStatusID] [int] IDENTITY(1,1) NOT NULL,
	[ObjectStatusDesc] [varchar](50) NOT NULL,
	[AvailableForEdit] [varchar](1) NULL,
	[AvailableToRole] [tinyint] NULL,
	[PartOfDevCycle] [varchar](1) NULL,
 CONSTRAINT [PK_LU_ObjectStatus] PRIMARY KEY CLUSTERED 
(
	[ObjectStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


