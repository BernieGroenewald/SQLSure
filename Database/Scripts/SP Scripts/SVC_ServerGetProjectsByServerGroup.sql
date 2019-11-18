USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetProjectsByServerGroup]    Script Date: 2017/02/14 07:39:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 7 Aug 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetProjectsByServerGroup](@ServerGroup varchar(50),
                                                           @IncludeInactive varchar(1))
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerGroupID int

	select @ServerGroupID = ServerGroupID
	  from SVC_LU_ServerGroup
	 where ServerGroupDesc = @ServerGroup

	if @IncludeInactive = 'Y'
	begin
		select ProjectID, 
			   ProjectName
		  from SVC_Project
		 where ServerGroupID = @ServerGroupID
	  order by ProjectName
	end
	else
	begin
		select ProjectID, 
			   ProjectName
		  from SVC_Project
		 where [Active] = 'Y'
		   and ServerGroupID = @ServerGroupID
	  order by ProjectName
  end
END


GO


