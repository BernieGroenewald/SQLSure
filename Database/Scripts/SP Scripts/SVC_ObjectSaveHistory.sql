USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ObjectSaveHistory]    Script Date: 2017/02/14 06:37:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 26 July 2016
-- Description:	Initial
-- =============================================

--Converted

CREATE PROCEDURE [dbo].[SVC_ObjectSaveHistory]( @ServerName varchar(50), 
											@ObjectStatus varchar(50),
											@UserName varchar(50), 
											@ObjectName varchar(255),
											@DatabaseName varchar(255),
											@ObjectText varchar(max),
											@Comment varchar(2048),
											@ProjectName varchar(50),
											@BackupSetName varchar(50),
											@ObjectType varchar(50),
											@DoDelete varchar(1),
											@VersionAtCheckOut varchar(1) = "N")
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerID int
	declare @StatusID int
	declare @ProjectID int
	declare @ServerAliasID int
	declare @UserID int
	declare @BackupSetID int
	declare @HistoryID int
	declare @ObjectVersion int

	select @UserID = UserID
	  from SVC_SystemUser
	 where UserName = @UserName

	select @ServerAliasID = ServerAliasID
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @ServerName

	select @ServerID = ServerID
	  from [SVC_Server]
	 where ServerAliasID = @ServerAliasID
	   and UserID = @UserID

	select @ObjectVersion = isnull(ObjectVersion, 0)
	  from SVC_ObjectHistory
	 where ObjectName = @ObjectName 
	   and DatabaseName = @DatabaseName
	   and HistoryDate = (select max(HistoryDate) from SVC_ObjectHistory where ObjectName = @ObjectName and DatabaseName = @DatabaseName) 

	if @ObjectVersion is null
	begin
		set @ObjectVersion = 0
	end

	if @ObjectStatus = 'Released to Production'
	begin
		set @ObjectVersion = @ObjectVersion + 1
	end
	
	if @ObjectStatus = 'Backup'
	begin
		select @StatusID = ObjectStatusID
		  from SVC_ObjectHistory
		 where ObjectName = @ObjectName 
		   and DatabaseName = @DatabaseName
		   and HistoryDate = (select max(HistoryDate) from SVC_ObjectHistory where ObjectName = @ObjectName and DatabaseName = @DatabaseName)

		if @StatusID is null
		begin
			select @StatusID = ObjectStatusID 
		      from SVC_LU_ObjectStatus 
		     where ObjectStatusDesc = @ObjectStatus
		end
	end
	else
	begin
		select @StatusID = ObjectStatusID 
		  from SVC_LU_ObjectStatus 
		 where ObjectStatusDesc = @ObjectStatus
	end

	set @ProjectID = 0

	select @ProjectID = isnull(ProjectID, 0)
	  from SVC_Project
	 where ProjectName = @ProjectName

	set @BackupSetID = 0

	if @BackupSetName <> ''
	begin
		select @BackupSetID = BackupSetID
		  from SVC_BackupSet
		 where BackupSetName = @BackupSetName
	end

	insert into SVC_ObjectHistory (ServerID, HistoryDate, ObjectStatusID, UserName, ObjectName, DatabaseName, ObjectText, Comment, ProjectID, ObjectVersion, VersionAtCheckOut)
	     select @ServerID, getdate(), @StatusID, @UserName, @ObjectName, @DatabaseName, @ObjectText, @Comment, @ProjectID, @ObjectVersion, @VersionAtCheckOut

	select @HistoryID =  ident_current('SVC_ObjectHistory')

	if @ProjectID > 0
	begin
		if not exists(select UserID from SVC_ProjectObject where ProjectID = @ProjectID and DBObjectName = @ObjectName and DBName = @DatabaseName)
		begin
			insert into SVC_ProjectObject (ProjectID, FileObjectID, DBName, DBObjectName, ObjectSequence, ObjectSelected, DateModified, UserID)
				 select @ProjectID,
						0,
						@DatabaseName,
						@ObjectName,
						0,
						'N',
						getdate(),
						@UserID
		end
	end

	if @BackupSetID > 0
	begin
		if @DoDelete = 'Y'
		begin
			delete from SVC_BackupSetObject
			      where BackupSetID = @BackupSetID
		end

		insert into SVC_BackupSetObject (BackupSetID, HistoryID, CreateDate, ObjectType)
		select @BackupSetID, @HistoryID, getdate(), @ObjectType
	end
END


GO


