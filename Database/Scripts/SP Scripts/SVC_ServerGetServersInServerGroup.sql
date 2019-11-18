USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServersInServerGroup]    Script Date: 2017/02/14 07:48:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetServersInServerGroup] (@ServerAlias varchar(50), 
                                                            @UserID int)
AS
BEGIN
	SET NOCOUNT ON;

	declare @PassPhrase nvarchar(128)
	declare @ServerGroupID int

	set @PassPhrase = ''

	exec SVC_SecurityKey @PassPhrase = @PassPhrase output

	select @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

    select S.ServerID, 
	       SL.ServerAliasDesc, 
		   S.ServerName, 
		   S.DBOwner, 
		   S.UserName, 
	       convert(varchar, DecryptByPassPhrase(@PassPhrase, S.Password)) as [Password], 
		   S.ServerActive, 
		   S.CreateDate, 
		   S.IntegratedSecurity,
		   SL.ServerAliasID,
		   SG.ServerGroupDesc
	  from SVC_LU_ServerAlias as SL inner join 
	       SVC_Server as S on SL.ServerAliasID = S.ServerAliasID inner join 
		   SVC_LU_ServerGroup as SG on SL.ServerGroupID = SG.ServerGroupID
	 where S.UserID = @UserID
	   and SG.ServerGroupID = @ServerGroupID
  order by SL.ServerGroupID, SL.ReleaseOrder
END

GO


