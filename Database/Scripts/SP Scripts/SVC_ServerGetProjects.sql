USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetProjects]    Script Date: 2017/02/14 07:38:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 7 Aug 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetProjects]
AS
BEGIN
	SET NOCOUNT ON;

	select ProjectID, 
	       ProjectName
	  from SVC_Project
	 where [Active] = 'Y'
  order by ProjectName
END


GO


