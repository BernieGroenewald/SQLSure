USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_InsertUpdateUserRole]    Script Date: 2017/02/14 07:33:55 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_InsertUpdateUserRole]
	@UserName varchar(50),
	@RoleName varchar(50),
	@SystemUser varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	declare @TmpStr varchar(100)
	declare @RoleId int
	
	if (select count(*) from SVC_SystemUserRole as SUR INNER JOIN
							 SVC_SystemRole as SR ON SUR.RoleId = SR.RoleId
					   where SR.RoleName = @RoleName
						 and SUR.UserName = @UserName) = 0
	begin
		--Insert

		select @RoleId = RoleId
		  from dbo.SVC_SystemRole
		 where RoleName = @RoleName

		insert into dbo.SVC_SystemUserRole(UserName, RoleId)
		     values(@UserName, @RoleId)

		set @TmpStr = 'New role ' + @RoleName + ' assigned to user ' + @UserName
		exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser

		select '0'
	end
	else
	begin
		--Remove the role from the user

		select @RoleId = RoleId
		  from dbo.SVC_SystemRole
		 where RoleName = @RoleName

		delete from dbo.SVC_SystemUserRole
		 where RoleId = @RoleId
		   and UserName = @UserName

		set @TmpStr = 'Role ' + @RoleName + ' removed from user ' + @UserName
		exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser

		select '1'
	end
END

GO


