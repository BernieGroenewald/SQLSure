USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_GetSystemUsers]    Script Date: 2017/02/14 06:50:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_GetSystemUsers]
AS
BEGIN
	SET NOCOUNT ON

	select UserID, UserName, QTNumber
	  from SVC_SystemUser
	  order by UserName
END

GO


