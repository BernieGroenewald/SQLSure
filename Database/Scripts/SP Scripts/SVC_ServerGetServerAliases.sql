USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerAliases]    Script Date: 2017/02/14 07:39:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetServerAliases]
AS
BEGIN
	SET NOCOUNT ON;

	select ServerAliasID, 
	       ServerAliasDesc 
	  from SVC_LU_ServerAlias 
  order by ServerAliasDesc
END


GO


