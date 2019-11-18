USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_DeleteUser]    Script Date: 2017/02/14 06:49:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_DeleteUser]
	@UserName varchar(50),
	@SystemUser varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	declare @TmpStr varchar(100)
	
	if (select count(*) from SVC_SystemUser where UserName = @UserName) > 0
	begin
		delete from dbo.SVC_SystemUser
		      where UserName = @UserName

		delete from dbo.SVC_SystemUserRole
		      where UserName = @UserName

		set @TmpStr = 'User ' + @UserName + ' deleted'
		exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser
	end
END


GO


