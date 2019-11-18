USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_ServerGetServersInServerGroup]    Script Date: 2017/03/15 09:30:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

alter PROCEDURE [dbo].[SVC_ServerGetServersInGroupDetail](@ServerGroup varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	select SA.ServerAliasID, 
		   SA.ServerAliasDesc, 
		   SA.ReleaseOrder, 
		   SA.ServerGroupID, 
		   SA.ServerRoleID, 
		   SG.ServerGroupDesc, 
		   SR.ServerRoleDesc,
		   SA.ServerName
	  from SVC_LU_ServerAlias as SA inner join
		   SVC_LU_ServerGroup as SG ON SA.ServerGroupID = SG.ServerGroupID inner join
		   SVC_LU_ServerRole as SR ON SA.ServerRoleID = SR.ServerRoleID
	where SG.ServerGroupDesc = @ServerGroup
	order by SA.ReleaseOrder
END