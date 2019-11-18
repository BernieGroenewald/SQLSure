USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerGroupFromAlias]    Script Date: 2017/02/14 07:40:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetServerGroupFromAlias](@AliasName varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerGroupID int

	select @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @AliasName

	select ServerGroupDesc
	  from SVC_LU_ServerGroup
	 where ServerGroupID = @ServerGroupID
END


GO


