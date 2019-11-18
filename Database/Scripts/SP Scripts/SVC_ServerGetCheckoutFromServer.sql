USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetCheckoutFromServer]    Script Date: 2017/02/14 07:36:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

create PROCEDURE [dbo].[SVC_ServerGetCheckoutFromServer](@DevServerAlias varchar(50))
AS
BEGIN
	SET NOCOUNT ON;

	declare @ServerGroupID int
	declare @ReleaseOrder tinyint

	select @ServerGroupID = ServerGroupID,
	       @ReleaseOrder = ReleaseOrder
	  from SVC_LU_ServerAlias
	 where ServerAliasDesc = @DevServerAlias

	select ServerAliasID, 
	       ServerAliasDesc 
	  from SVC_LU_ServerAlias 
	 where ServerGroupID = @ServerGroupID
	   and ReleaseOrder > @ReleaseOrder
  order by ReleaseOrder
END


GO


