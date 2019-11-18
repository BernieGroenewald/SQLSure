USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_Security_GetSystemRoles]    Script Date: 2017/02/14 06:50:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SVC_Security_GetSystemRoles]
AS
BEGIN
	SET NOCOUNT ON

	select * from dbo.SVC_SystemRole order by RoleName
END


GO


