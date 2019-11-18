USE [SQLSourceControl]
GO

/****** Object:  Table [dbo].[SVC_SystemPerson]    Script Date: 2017/02/14 07:57:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SVC_SystemPerson](
	[SystemPersonId] [int] IDENTITY(1,1) NOT NULL,
	[SystemEntityType] [varchar](20) NOT NULL,
	[PersonId] [int] NOT NULL,
 CONSTRAINT [PK_SystemPerson] PRIMARY KEY CLUSTERED 
(
	[SystemPersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


