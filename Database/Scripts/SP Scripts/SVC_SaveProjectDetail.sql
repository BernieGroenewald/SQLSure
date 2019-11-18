USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SaveProjectDetail]    Script Date: 2017/02/14 06:39:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_SaveProjectDetail](@ProjectName varchar(50),
                                               @FileObject varchar(max),
											   @FileObjectDesc varchar(255),
											   @DBName varchar(255),
											   @DBObjectName varchar(255),
											   @ObjectSequence int,
											   @ObjectSelected varchar(1),
										       @UserID int,
											   @DoDelete varchar(1),
											   @ProjectActive varchar(1))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ProjectID int
	declare @FileObjectID int

	set @FileObjectID = 0

	if exists(select ProjectID from SVC_Project where ProjectName = @ProjectName)
	begin
		select @ProjectID = ProjectID 
		  from SVC_Project 
		 where ProjectName = @ProjectName

		if ltrim(@FileObject) <> ''
		begin
			insert into SVC_FileObject (FileObject, ObjectDescription)
			     select @FileObject, @FileObjectDesc

			select @FileObjectID = ident_current('SVC_FileObject')
		end


		if @DoDelete = 'Y'
		begin
			update SVC_Project
			   set Active = @ProjectActive
			 where ProjectID = @ProjectID

			insert into SVC_ProjectObjectHistory (ProjectObjectID, ProjectID, FileObjectID, DBName, DBObjectName, ObjectSequence, ObjectSelected, DateModified, UserID)
			     select ProjectObjectID, ProjectID, FileObjectID, DBName, DBObjectName, ObjectSequence, ObjectSelected, DateModified, UserID
			       from SVC_ProjectObject
				  where ProjectID = @ProjectID

			delete from SVC_ProjectObject
				  where ProjectID = @ProjectID
		end

		insert into SVC_ProjectObject(ProjectID, FileObjectID, DBName, DBObjectName, ObjectSequence, ObjectSelected, DateModified, UserID)
		     select @ProjectID,
			        @FileObjectID, 
					@DBName,
					@DBObjectName,
					@ObjectSequence,
					@ObjectSelected,
					getdate(),
					@UserID
	end
END


GO


