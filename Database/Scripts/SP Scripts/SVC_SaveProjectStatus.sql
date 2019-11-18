USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_SaveProjectStatus]    Script Date: 2017/02/14 06:39:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 27 August 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_SaveProjectStatus](@ProjectName varchar(50),
                                               @ObjectStatus varchar(50),
											   @Comment varchar(2048),
										       @UserID int)
	
AS
BEGIN
	SET NOCOUNT ON;

	declare @StatusID int
	declare @ProjectID int

	select @StatusID = ObjectStatusID 
	  from SVC_LU_ObjectStatus 
	 where ObjectStatusDesc = @ObjectStatus

	select @ProjectID = isnull(ProjectID, 1)
	  from SVC_Project
	 where ProjectName = @ProjectName

	if exists(select ProjectID from SVC_Project where ProjectID = @ProjectID)
	begin
		update SVC_Project
		   set ObjectStatusID = @StatusID,
		       Comment = @Comment,
			   UserID = @UserID
		 where ProjectID = @ProjectID
	end
END


GO


