USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SaveBackupSetObjectType]    Script Date: 2017/02/14 06:38:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_SaveBackupSetObjectType](@BackupSetName varchar(50),
                                                   @ObjectType varchar(255),
										           @DoDelete varchar(1))
	
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

	if @DoDelete = 'Y'
	begin
		delete from SVC_BackupSetObjectType
		      where BackupSetID = @BackupSetID
	end

	insert into SVC_BackupSetObjectType (BackupSetID, ObjectType)
	     select @BackupSetID, @ObjectType
END


GO


