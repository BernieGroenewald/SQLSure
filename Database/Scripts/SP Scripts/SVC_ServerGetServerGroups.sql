USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerGroups]    Script Date: 2017/02/14 07:47:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetServerGroups]
AS
BEGIN
	SET NOCOUNT ON;

	select ServerGroupID, 
	       ServerGroupDesc
	  from SVC_LU_ServerGroup
  order by ServerGroupDesc
END


GO


