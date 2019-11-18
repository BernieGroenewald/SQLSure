USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_SystemRolesByUsers]    Script Date: 2017/02/14 07:34:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_SystemRolesByUsers]
	@RoleName varchar(50),
	@FunctionName varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	select SUR.UserName, SR.RoleName
      from SVC_SystemUserRole as SUR INNER JOIN
           SVC_SystemRole as SR ON SUR.RoleId = SR.RoleId
  order by SUR.UserName, SR.RoleName
END


GO


