USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerGroupID]    Script Date: 2017/02/14 07:40:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetServerGroupID](@GroupName varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	select ServerGroupID
	  from SVC_LU_ServerGroup
	 where ServerGroupDesc = @GroupName
END


GO


