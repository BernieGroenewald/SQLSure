USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServerDetail]    Script Date: 2017/02/14 07:39:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetServerDetail](@ServerAlias varchar(50), @UserID int)
AS
BEGIN
	SET NOCOUNT ON;

	declare @PassPhrase nvarchar(128)
	declare @ServerAliasID int

	select @ServerAliasID = ServerAliasID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	set @PassPhrase = ''

	exec SVC_SecurityKey @PassPhrase = @PassPhrase output

	if exists(select ServerID from [SVC_Server] where ServerAliasID = @ServerAliasID and UserID = @UserID)
	begin
		select S.ServerID, 
		       S.ServerName, 
			   S.DBOwner, 
			   S.UserName, 
			   convert(varchar, DecryptByPassPhrase(@PassPhrase, S.Password)) as [Password], 
			   S.IntegratedSecurity, 
			   SR.ServerRoleDesc, 
               S.ServerRoleID,
			   S.ServerAliasID as ServerAliasID
          from SVC_Server as S inner join SVC_LU_ServerRole as SR on S.ServerRoleID = SR.ServerRoleID
		 where S.ServerAliasID = @ServerAliasID
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
			   0 as ServerAliasID
	end
END


GO


