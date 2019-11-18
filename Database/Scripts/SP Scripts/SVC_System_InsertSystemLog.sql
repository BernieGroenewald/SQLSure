USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_System_InsertSystemLog]    Script Date: 2017/02/14 07:49:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SVC_System_InsertSystemLog]
	@LogType varchar(50),
	@LogDescription varchar(1024),
	@UserName varchar(50)

AS
BEGIN
	SET NOCOUNT ON

	insert into dbo.SVC_SystemLog (LogType, LogDateTime, LogDescription, UserName)
	     values(@LogType, getdate(), @LogDescription, @UserName)
END


GO


