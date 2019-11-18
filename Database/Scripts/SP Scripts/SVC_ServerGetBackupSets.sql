USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetBackupSets]    Script Date: 2017/02/14 07:35:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 7 Aug 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetBackupSets]
AS
BEGIN
	SET NOCOUNT ON;

	select BackupSetID, 
	       BackupSetName
	  from SVC_BackupSet
	 where [Active] = 'Y'
  order by BackupSetName
END


GO


