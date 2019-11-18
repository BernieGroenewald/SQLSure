USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_ChangePassword]    Script Date: 2017/02/14 06:40:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_ChangePassword]
	@UserName varchar(50),
	@UserPassword varchar(50),
	@SystemUser varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	declare @TmpStr varchar(100)

	update dbo.SVC_SystemUser
	   set UserPassword = @UserPassword
	 where UserName = @UserName

	set @TmpStr = 'System user ' + @UserName + ' password changed'
	exec dbo.SVC_System_InsertSystemLog 'Security', @TmpStr, @SystemUser
END

GO


