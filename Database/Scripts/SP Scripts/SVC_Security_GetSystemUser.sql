USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_GetSystemUser]    Script Date: 2017/02/14 06:50:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_GetSystemUser]
	@QTNumber varchar(10),
	@CallingForm varchar(50)
AS
BEGIN
	SET NOCOUNT ON

	insert into SVC_UsageLog (QTNumber, CallingForm, Logdate)
	select @QTNumber, @CallingForm, getdate()

	select UserID, UserName, QTNumber
	  from SVC_SystemUser
	 where QTNumber = @QTNumber	
END

GO


