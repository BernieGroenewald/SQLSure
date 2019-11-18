USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SystemGetDatabases]    Script Date: 2017/02/14 07:50:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_SystemGetDatabases]
AS
BEGIN
	SET NOCOUNT ON;

	select Name
      from Sys.Databases
  order by Name
END

GO


