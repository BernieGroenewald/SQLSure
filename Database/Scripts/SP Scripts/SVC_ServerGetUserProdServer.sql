USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetUserProdServer]    Script Date: 2017/03/16 01:53:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

ALTER PROCEDURE [dbo].[SVC_ServerGetUserProdServer](@ServerAlias varchar(50), @UserID int)
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerAliasID int
	declare @ProdServerAliasID int
	declare @ServerGroupID int
	
	select @ServerAliasID = ServerAliasID,
	       @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	select @ProdServerAliasID = SA.ServerAliasID
      from SVC_LU_ServerAlias as SA inner join
           SVC_LU_ServerGroup as SG on SA.ServerGroupID = SG.ServerGroupID inner join
           SVC_Server as S on SA.ServerAliasID = S.ServerAliasID inner join
           SVC_LU_ServerRole as SR on SA.ServerRoleID = SR.ServerRoleID
	 where SG.ServerGroupID = @ServerGroupID
	   and SR.ServerRoleDesc = 'Production'
	   and S.UserID = @UserID

	if exists(select ServerID from [SVC_Server] where ServerAliasID = @ProdServerAliasID and UserID = @UserID)
	begin
		select S.ServerID, 
		       SA.ServerName, 
			   S.DBOwner, 
			   S.UserName, 
			   convert(varchar, S.Password) as [Password], 
			   S.IntegratedSecurity, 
			   SR.ServerRoleDesc, 
               SA.ServerRoleID,
			   S.ServerAliasID as ServerAliasID,
			   SA.ServerAliasDesc
          from SVC_Server as S inner join 
		       SVC_LU_ServerAlias as SA on SA.ServerAliasID = S.ServerAliasID inner join
			   SVC_LU_ServerRole as SR on SA.ServerRoleID = SR.ServerRoleID
		 where S.ServerAliasID = @ProdServerAliasID
		   and S.UserID = @UserID
	end
	else
	begin
		select '' as ServerID, 
		       '' as ServerName, 
			   '' as DBOwner, 
			   '' as UserName, 
			   '' as Password,
			   '' as IntegratedSecurity,
			   '' as ServerRoleDesc,
			   0 as ServerRoleID,
			   0 as ServerAliasID,
			   '' as ServerAliasDesc
	end
END

