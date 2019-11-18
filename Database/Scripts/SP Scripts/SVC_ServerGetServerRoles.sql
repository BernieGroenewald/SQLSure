USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerRoles]    Script Date: 2017/02/14 07:48:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetServerRoles]
AS
BEGIN
	SET NOCOUNT ON;

	select ServerRoleID, 
	       ServerRoleDesc 
	  from SVC_LU_ServerRole 
  order by ServerRoleDesc
END


GO


