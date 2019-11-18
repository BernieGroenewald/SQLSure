USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_GetSystemFunctions]    Script Date: 2017/02/14 06:49:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_GetSystemFunctions]
AS
BEGIN
	SET NOCOUNT ON

	select * from SVC_SystemFunction order by Rank
END


GO


