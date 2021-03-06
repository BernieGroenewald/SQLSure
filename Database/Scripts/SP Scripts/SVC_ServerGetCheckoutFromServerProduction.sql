USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetCheckoutFromServer]    Script Date: 2017/08/31 03:05:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetCheckoutFromServerProduction](@DevServerAlias varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerGroupID int
	declare @ServerRoleId int
	
	select @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @DevServerAlias

	select @ServerRoleId = ServerRoleID
	  from SVC_LU_ServerRole
	 where ServerRoleDesc = 'Production'

	select ServerAliasID, 
	       ServerAliasDesc 
	  from SVC_LU_ServerAlias 
	 where ServerGroupID = @ServerGroupID
       and ServerRoleId = @ServerRoleId
END

