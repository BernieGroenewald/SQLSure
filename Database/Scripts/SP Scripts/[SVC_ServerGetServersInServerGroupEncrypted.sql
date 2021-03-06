USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServersInServerGroup]    Script Date: 2017/03/16 01:05:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

alter PROCEDURE [dbo].[SVC_ServerGetServersInServerGroupEncrypted] (@ServerAlias varchar(50), 
                                                            @UserID int)
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerGroupID int

	select @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

    select S.ServerID, 
	       SL.ServerAliasDesc, 
		   SL.ServerName, 
		   S.DBOwner, 
		   S.UserName, 
	       convert(varchar, S.Password) as [Password], 
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
