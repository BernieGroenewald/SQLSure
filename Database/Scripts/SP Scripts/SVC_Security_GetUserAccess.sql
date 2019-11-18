USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_GetUserAccess]    Script Date: 2017/02/14 07:32:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_GetUserAccess]
	@UserName varchar(50),
	@SystemArea varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	select SU.UserName, SF.FunctionName
	  from SVC_SystemUser SU INNER JOIN
           SVC_SystemUserRole SUR ON SU.UserName = SUR.UserName INNER JOIN
           SVC_RoleFunction RF ON SUR.RoleId = RF.RoleId INNER JOIN
           SVC_SystemFunction SF ON RF.SystemFunctionId = SF.SystemFunctionId
	 where SU.UserName = @UserName
	   and SF.Level1 = @SystemArea

	if @@rowcount = 0
	begin
		select '' as UserName, '' as FunctionName
	end
END

GO


