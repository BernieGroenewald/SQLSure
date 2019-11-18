USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_InsertUpdateRoleFunction]    Script Date: 2017/02/14 07:33:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_InsertUpdateRoleFunction]
	@RoleName varchar(50),
	@FunctionName varchar(50),
	@SystemUser varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	declare @TmpStr varchar(100)
	declare @RoleId int
	declare @FunctionId int

	if (select count(*) from SVC_RoleFunction as RF INNER JOIN
							 SVC_SystemRole as SR ON RF.RoleId = SR.RoleId INNER JOIN
                             SVC_SystemFunction as SF ON RF.SystemFunctionId = SF.SystemFunctionId
					   where SR.RoleName = @RoleName
						 and SF.FunctionName = @FunctionName) = 0
	begin
		--Insert

		select @RoleId = RoleId
		  from dbo.SVC_SystemRole
		 where RoleName = @RoleName

		select @FunctionId = SystemFunctionId
		  from dbo.SVC_SystemFunction
		 where FunctionName = @FunctionName

		insert into dbo.SVC_RoleFunction(RoleId, SystemFunctionId)
		     values(@RoleId, @FunctionId)

		set @TmpStr = 'New function ' + @FunctionName + ' added to role ' + @RoleName
		exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser

		select '0'
	end
	else
	begin
		--Remove the function from the role

		select @RoleId = RoleId
		  from dbo.SVC_SystemRole
		 where RoleName = @RoleName

		select @FunctionId = SystemFunctionId
		  from dbo.SVC_SystemFunction
		 where FunctionName = @FunctionName

		delete from dbo.SVC_RoleFunction
		 where RoleId = @RoleId
		   and SystemFunctionId = @FunctionId

		set @TmpStr = 'Function ' + @FunctionName + ' removed from role ' + @RoleName
		exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser

		select '1'
	end
END

GO


