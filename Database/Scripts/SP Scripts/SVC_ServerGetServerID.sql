USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerID]    Script Date: 2017/02/14 07:47:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetServerID](@ServerAlias varchar(50), @UserID int)
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerAliasID int

	select @ServerAliasID = ServerAliasID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	if exists(select ServerID from [SVC_Server] where ServerAliasID = @ServerAliasID and UserID = @UserID)
	begin
		select ServerID 
		  from [SVC_Server] 
		 where ServerAliasID = @ServerAliasID
		   and UserID = @UserID
	end
	else
	begin
		select 0 as ServerID 
	end
END


GO


