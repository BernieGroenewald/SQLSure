USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SaveBackupSet]    Script Date: 2017/02/14 06:37:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_SaveBackupSet](@BackupSetName varchar(50),
                                           @BackupSetDesc varchar(1024),
										   @UserID int,
										   @ServerAlias varchar(50),
										   @BackupSetActive varchar(1))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerAliasID int

	select @ServerAliasID = ServerAliasID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerAlias

	if not exists(select BackupSetID from SVC_BackupSet where BackupSetName = @BackupSetName)
	begin
		insert into SVC_BackupSet(BackupSetName, BackupSetDesc, UserID, CreateDate, [Active], ServerAliasID)
		     select @BackupSetName, @BackupSetDesc, @UserID, getdate(), @BackupSetActive, @ServerAliasID
	end
	else
	begin
		update SVC_BackupSet
		   set BackupSetDesc = @BackupSetDesc,
		       [Active] = @BackupSetActive
		 where BackupSetName = @BackupSetName
	end
END


GO


