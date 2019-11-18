USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_GetBackupSetDatabase]    Script Date: 2017/02/14 06:29:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_GetBackupSetDatabase](@BackupSetName varchar(50))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @BackupSetID int

	set @BackupSetID = 0

	if @BackupSetName <> ''
	begin
		select @BackupSetID = BackupSetID
		  from SVC_BackupSet
		 where BackupSetName = @BackupSetName
	end

	select DatabaseName
	  from SVC_BackupSetDatabase
	 where BackupSetID = @BackupSetID
  order by DatabaseName
END


GO


