USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_AllSystemRolesByUsers]    Script Date: 2017/02/14 06:39:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[SVC_Security_AllSystemRolesByUsers]
AS
BEGIN
	SET NOCOUNT ON

	select SUR.UserName, SR.RoleName
      from SVC_SystemUserRole as SUR INNER JOIN
           SVC_SystemRole as SR ON SUR.RoleId = SR.RoleId
  order by SUR.UserName, SR.RoleName
END


GO


