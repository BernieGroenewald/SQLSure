USE [SQLSourceControl]
GO
/****** Object:  StoredProcedure [dbo].[SVC_GetServerRole]    Script Date: 2017/03/16 09:32:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_GetUserServerRole]( @ServerName varchar(255),
                                                @UserID int)
AS
BEGIN
	SET NOCOUNT ON;

	set @ServerName = replace(@ServerName, '\\', '\')

	select SR.ServerRoleDesc, 
	       SA.ServerName, 
		   SA.ServerAliasDesc
      from SVC_LU_ServerAlias as SA inner join
           SVC_LU_ServerRole as SR on SA.ServerRoleID = SR.ServerRoleID inner join
           SVC_Server as S on SA.ServerAliasID = S.ServerAliasID
     where @ServerName like '%' + SA.ServerName + '%'
       and S.UserID = @UserID
END
