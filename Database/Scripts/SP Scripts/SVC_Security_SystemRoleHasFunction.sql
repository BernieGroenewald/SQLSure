USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_SystemRoleHasFunction]    Script Date: 2017/02/14 07:34:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_SystemRoleHasFunction]
	@RoleName varchar(50),
	@FunctionName varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	select SR.RoleName, SF.FunctionName
	  from SVC_RoleFunction as RF INNER JOIN
           SVC_SystemRole as SR on RF.RoleId = SR.RoleId INNER JOIN
           SVC_SystemFunction as SF on RF.SystemFunctionId = SF.SystemFunctionId
	 where SR.RoleName = @RoleName 
	   and SF.FunctionName = @FunctionName
END


GO


