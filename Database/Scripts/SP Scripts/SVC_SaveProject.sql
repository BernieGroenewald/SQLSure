USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SaveProject]    Script Date: 2017/02/14 06:38:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_SaveProject](@ProjectName varchar(50),
                                         @ProjectDesc varchar(1024),
										 @UserID int,
										 @ServerGroup varchar(50),
										 @ProjectActive varchar(1))
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerGroupID int

	select @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerGroup
	 where ServerGroupDesc = @ServerGroup

	if not exists(select ProjectID from SVC_Project where ProjectName = @ProjectName)
	begin
		insert into SVC_Project(ProjectName, ProjectDesc, UserID, CreateDate, [Active], ServerGroupID)
		     select @ProjectName, @ProjectDesc, @UserID, getdate(), @ProjectActive, @ServerGroupID
	end
END


GO


