USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_InsertRole]    Script Date: 2017/02/14 07:33:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_InsertRole]
	@RoleName varchar(50),
	@SystemUser varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	declare @TmpStr varchar(100)
	declare @RoleId int
	
	if (select count(*) from SVC_SystemRole
					   where RoleName = @RoleName) = 0
	begin
		--Insert

		insert into dbo.SVC_SystemRole(RoleName)
		     values(@RoleName)

		set @TmpStr = 'New role ' + @RoleName + ' created'
		exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser
	end
END


GO


