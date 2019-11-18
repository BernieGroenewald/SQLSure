USE [SQLSourceControl]
GO

/****** Object:  StoredProcedure [dbo].[SVC_ServerGetObjectStatus]    Script Date: 2017/02/14 07:37:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Bernie Groenewald
-- Create date: 20 July 2016
-- Description:	Initial
-- =============================================

CREATE PROCEDURE [dbo].[SVC_ServerGetObjectStatus]( @QTNumber varchar(10))
AS
BEGIN
	SET NOCOUNT ON;

	declare @MaxRole tinyint

	set @MaxRole = 0

	select @MaxRole = max(SUR.RoleId)
      from SVC_SystemUser as SU inner join
           SVC_SystemUserRole as SUR on SU.UserID = SUR.UserID inner join
           SVC_SystemRole as SR on SUR.RoleId = SR.RoleId
     where SU.QTNumber = @QTNumber

	 if @MaxRole = 0
	 begin
		set @MaxRole = 1
	 end

	select ObjectStatusID, 
	       ObjectStatusDesc 
	  from SVC_LU_ObjectStatus
	 where AvailableToRole <= @MaxRole
  order by ObjectStatusDesc
END

GO


