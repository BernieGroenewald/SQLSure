USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_RolesByRole]    Script Date: 2017/02/14 07:34:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SVC_Security_RolesByRole]
	@RoleName varchar(50),
	@FunctionName varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	select SR.RoleName, SUR.UserName
      from SVC_SystemUserRole as SUR INNER JOIN
           SVC_SystemRole as SR ON SUR.RoleId = SR.RoleId
  order by SR.RoleName, SUR.UserName
END


GO


