USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_GetBackupSetObjectType]    Script Date: 2017/02/14 06:29:48 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_GetBackupSetObjectType](@BackupSetName varchar(50))
	
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

	select ObjectType
	  from SVC_BackupSetObjectType
	 where BackupSetID = @BackupSetID
  order by ObjectType
END


GO


